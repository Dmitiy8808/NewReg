using Microsoft.AspNetCore.Components;

namespace Reg.Client.Components
{
    public partial class UserCard
    {
        [Parameter] public string Email { get; set; }
        [Parameter] public string Class { get; set; }
        private char FirstLetterOfEmail { get; set; }


        protected override async Task OnInitializedAsync()
        {
            FirstLetterOfEmail = Email[0];
        }

        // private async Task LoadDataAsync()
        // {
        //     var state = await _stateProvider.GetAuthenticationStateAsync();
        //     var user = state.User;

        //     this.Email = user.GetEmail().Replace(".com", string.Empty);
        //     this.FirstName = user.GetFirstName();
        //     this.SecondName = user.GetLastName();
        //     if (this.FirstName.Length > 0)
        //     {
        //         FirstLetterOfName = FirstName[0];
        //     }
        //     var UserId = user.GetUserId();
        //     var imageResponse = await _accountManager.GetProfilePictureAsync(UserId);
        //     if (imageResponse.Succeeded)
        //     {
        //         ImageDataUrl = imageResponse.Data;
        //     }
        // }
    }
}