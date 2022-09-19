
namespace gov.minahasa.sitimou.Helper
{
    internal class ComboBoxHelper
    {
        private string _text;
        private string _tag;

        public ComboBoxHelper(string sText, string sValue)
        {
            // Me.StringDesc = desc
            // Me.ValueID = value
            this._text = sText;
            this._tag = sValue;
        }

        public string Description
        {
            get => _text;
            set => _text = value;
        }

        public string Value
        {
            get => _tag;
            set => _tag = value;
        }
    }
}
