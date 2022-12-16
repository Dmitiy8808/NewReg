using System.Reflection;
using System.Text;
using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Server.Repository.RepositoryEtensions
{
    public static class RepositoryRequestExtensions
    {
        public static IQueryable<RequestAbonent> Search(this IQueryable<RequestAbonent> requests, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return requests;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return requests.Where(p => p.Person.LastName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<RequestAbonent> Sort(this IQueryable<RequestAbonent> requests, string orderByQueryString) 
        { 
            if (string.IsNullOrWhiteSpace(orderByQueryString)) 
                return requests.OrderBy(e => e.CreationTime); 
            
            var orderParams = orderByQueryString.Trim().Split(','); 
            var propertyInfos = typeof(RequestAbonent).GetProperties(BindingFlags.Public | BindingFlags.Instance); 
            var orderQueryBuilder = new StringBuilder(); 
            
            foreach (var param in orderParams) 
            { 
                if (string.IsNullOrWhiteSpace(param)) 
                    continue; 
                
                var propertyFromQueryName = param.Split(" ")[0]; 
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase)); 
                
                if (objectProperty == null) 
                    continue; 
                
                var direction = param.EndsWith(" desc") ? "descending" : "ascending"; 
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, "); 
            } 
            
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); 
            if (string.IsNullOrWhiteSpace(orderQuery)) 
                return requests.OrderBy(e => e.CreationTime); 
            
            return requests.OrderBy(orderQuery); 
        }
    }
}