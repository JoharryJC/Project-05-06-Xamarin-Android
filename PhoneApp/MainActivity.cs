using Android.App;
using Android.Widget;
using Android.OS;

namespace PhoneApp
{
    [Activity(Label = "Phone App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> PhoneNumbers = new System.Collections.Generic.List<string>(); 


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);

            //var miMensajeText = FindViewById<TextView>(Resource.Id.miMensajeText);

            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton); 
            var ValidarFistButton = FindViewById<Button>(Resource.Id.ValidaFirstButton);

            CallButton.Enabled = false;
            var TranslatedNumber = string.Empty;

            TranslateButton.Click += (object sender, System.EventArgs e) =>
            {
                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    //No hay numero a llamar
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    //Hay un posible numero telefonico a llamar
                    CallButton.Text = $"Llamar al {TranslatedNumber}";
                    CallButton.Enabled = true;
                }
            };

            CallButton.Click += (object sender, System.EventArgs e) =>
            {
                //Intentar marcar el numero telefonico 
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al numero {TranslatedNumber}?");

                CallDialog.SetNeutralButton("Llamar",
                    delegate
                    {
                        PhoneNumbers.Add(TranslatedNumber);
                        CallHistoryButton.Enabled = true;

                        var CallIntent =
                            new Android.Content.Intent(
                                Android.Content.Intent.ActionCall);
                        CallIntent.SetData(Android.Net.Uri.Parse($"tel: {TranslatedNumber}"));
                        StartActivity(CallIntent);
                    });

                CallDialog.SetNegativeButton("Cancelar", delegate { });

                //Mostrar el cuadro de diagolo al usuario y esperar la respuesta 
                CallDialog.Show();
            };

            CallHistoryButton.Click += (sender, e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(CallHistoryActivity));
                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(Intent); 

            };


            ValidarFistButton.Click += (object sender, System.EventArgs e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(ValidatorjcActivity));
                StartActivity(Intent);

            };

            //Validate();

            //miMensajeText.Text = $"Satisfactoriamente\nJoharry Correa\ncodigo mmmmm-alak\nXamarinDiplomado3.0-Lab05";

        }

        private async void Validate()
        {
            var miMensajeText = FindViewById<TextView>(Resource.Id.miMensajeText);

            var ServiceClient = new SALLab06.ServiceClient();
            string StudentEmail = @"energycuenta@yahoo.com";
            string PasswordStudent = "********";  // lo acabo de comentar 2018 
            string myDevice = Android.Provider.Settings.Secure.GetString(
                    ContentResolver,
                    Android.Provider.Settings.Secure.AndroidId);
            var Result = await ServiceClient.ValidateAsync(StudentEmail, PasswordStudent, myDevice);
            miMensajeText.Text = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";

            //miMensajeText.Text = $"Satisfactoriamente\nJoharry Correa\ncodigo mmmmm-alak\nXamarinDiplomado3.0-Lab05";

            //var miMensajeText = FindViewById<TextView>(Resource.Id.miMensajeText);

            //miMensajeText.Text = $"Satisfactoriamente\nJoharry Correa\ncodigo mmmmm-alak\nXamarinDiplomado3.0-Lab05";
        }
    }
}

