using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    [Activity(Label = "@string/ValidatorJC", Icon = "@drawable/icon")]
    public class ValidatorjcActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Validatorjc);

            //var ValidarCredButton2 = FindViewById<Button>(Resource.Id.validarCredButton);

            var botonValidador = FindViewById<Button>(Resource.Id.validarCredButton);
            botonValidador.Click += (object sender, System.EventArgs e) =>
            {
                Validate2();
            };

            //miResultado.Text = "Hola Amigo";

            //ValidarCredButton2.Click += (object sender, System.EventArgs e) =>
            //{
            //    Validate();
            //};

        }

        private async void Validate2()
        {
            var miCorreo = FindViewById<EditText>(Resource.Id.textEmailAddress);
            var miPassword = FindViewById<EditText>(Resource.Id.textPassword);

            var miResultado = FindViewById<TextView>(Resource.Id.miMensajeText2);

            var ServiceClient = new SALLab06.ServiceClient();

            string StudentEmail = miCorreo.Text.Trim();
            string PasswordStudent = miPassword.Text.Trim();

            string myDevice = Android.Provider.Settings.Secure.GetString(
                    ContentResolver,
                    Android.Provider.Settings.Secure.AndroidId);
            var Result = await ServiceClient.ValidateAsync(StudentEmail, PasswordStudent, myDevice);
            miResultado.Text = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
        }


    }
}