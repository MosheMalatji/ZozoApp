using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZozoApp.Services;

namespace ZozoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            if (!EntPassword.Text.Equals(EntConfirmPassword.Text))
            {
                await DisplayAlert("Password Mismatch", "Eh!Check your password again maybe ?", "Cancel");
            }
            else
            {
                var response = await ApiServices.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
                if (response)
                {
                    await DisplayAlert("Hi", "Your Account Has Been Created", "Cool");
                }
                else
                {
                    await DisplayAlert("Hi", "Something Went Wrong", "Cancel");
                }
            }
        }
    }
}