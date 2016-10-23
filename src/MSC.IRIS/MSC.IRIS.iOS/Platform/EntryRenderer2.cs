using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (Entry), typeof (MSC.IRIS.iOS.EntryRenderer2))]

namespace MSC.IRIS.iOS
{
    public class EntryRenderer2 : EntryRenderer
    {
        public EntryRenderer2 ()
        {
        }

        protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged (e);

            if (this.Control == null)
                return;

            Control.ClearButtonMode = UITextFieldViewMode.Always;
            Control.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
            Control.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
            Control.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization
        }
    }
}
