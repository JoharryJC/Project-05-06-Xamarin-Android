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
    public class PhoneTranslator
    {
        string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string Numbers = "222333444555666777888999";

        public string ToNumber(string alfanumericoPhoneNumber) {
            var NumericPhoneNumber = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(alfanumericoPhoneNumber)) {
                alfanumericoPhoneNumber = alfanumericoPhoneNumber.ToUpper();
                foreach (var c in alfanumericoPhoneNumber) {
                    if ("0123456789".Contains(c))
                    {
                        NumericPhoneNumber.Append(c);
                    }
                    else {
                        var Index = Letters.IndexOf(c);
                        if (Index >= 0) {
                            NumericPhoneNumber.Append(Numbers[Index]);
                        }
                    }
                }
            }

            return NumericPhoneNumber.ToString(); 

        }

    }
}