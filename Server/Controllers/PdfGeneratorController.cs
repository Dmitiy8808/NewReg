using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Server.Utility;

namespace Server.Controllers
{
    [Route("api/pdfcreator")]
    [ApiController]
    public class PdfGeneratorController : ControllerBase
    {
        private string htmlContent { get; set; }
        private readonly IConverter _converter;

        public PdfGeneratorController(IConverter converter)
        {
 
            _converter = converter;
            
        }

        [HttpPost]
        public IActionResult CreatePDF(RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            

            if (requestAbonentUpdateDto.IsJuridical)
            {
                htmlContent = UlTemplateGenerator.GetHTMLString(requestAbonentUpdateDto); 
            }
            else
            {
                htmlContent = FlTemplateGenerator.GetHTMLString(requestAbonentUpdateDto);
            }
            var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 10 },
            DocumentTitle = "Заявление"
        };
        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = htmlContent,
            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
        };
        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };
        var file = _converter.Convert(pdf);
        return File(file, "application/pdf", "Zayavlenie.pdf");
        }

        [Route("dover")]
        [HttpPost]
        public IActionResult CreateDovPDF(RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 10 },
            DocumentTitle = "Доверенность"
        };
        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = DoverTemplateGenerator.GetHTMLString(requestAbonentUpdateDto),
            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
        };
        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };
        var file = _converter.Convert(pdf);
        return File(file, "application/pdf", "Doverennost.pdf");
        }


        
    }
}