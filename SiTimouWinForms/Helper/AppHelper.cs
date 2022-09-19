using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace gov.minahasa.sitimou.Helper
{
    internal class AppHelper
    {
        private delegate ListView.ListViewItemCollection GetItems(ListView lstview);

        #region === Control Stuff ===

        public static void SetBackgroundColor(Control.ControlCollection control)
        {
            foreach (Control c in control)
            {
                foreach (var tb in control.OfType<TextBoxExt>())
                {
                    //tb.Text = string.Empty;
                    tb.BackColor = Globals.PrimaryBgColor;
                }

                foreach (var frm in control.OfType<Form>())
                {
                    frm.BackColor = Globals.PrimaryBgColor;
                }
            }
        }


        #endregion

        #region === Input stuff ===

        public static Func<int, string> ToRoman = x =>
        {
            var numerals = new[]
            {
                new { text = "L", value = 50, },
                new { text = "XL", value = 40, },
                new { text = "X", value = 10, },
                new { text = "IX", value = 9, },
                new { text = "V", value = 5, },
                new { text = "IV", value = 4, },
                new { text = "I", value = 1, },
            };

            return
                numerals.Aggregate(new { text = "", value = x }, (a, n) =>
                {
                    var text = a.text;
                    var value = a.value;
                    while (value >= n.value)
                    {
                        text += n.text;
                        value -= n.value;
                    }

                    return new { text, value };
                }).text;
        };

        public static void ClearInput(Control.ControlCollection control)
        {
            foreach (Control c in control)
            {
                foreach (var tb in control.OfType<TextBox>())
                {
                    tb.Text = string.Empty;
                }

                foreach (var cb in control.OfType<ComboBox>())
                {
                    cb.SelectedIndex = -1;
                }

                foreach (Control ct in control)
                {
                    ClearInput(ct.Controls);
                }
            }
        }

        public bool IsNumber(char ch, string text)
        {
            var res = true;
            var nfi = new CultureInfo("id-ID", false).NumberFormat;
            // var decimalChar = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            var decimalChar = Convert.ToChar(nfi.NumberDecimalSeparator);

            //check if it´s a decimal separator and if doesn´t already have one in the text string
            if (ch == decimalChar && text.IndexOf(decimalChar) != -1)
            {
                res = false;
                return res;
            }

            //check if it´s a digit, decimal separator and backspace
            if (!char.IsDigit(ch) && ch != decimalChar && ch != (char)Keys.Back)
                res = false;

            return res;
        }

        public bool IsNumberOnly(char ch)
        {
            return char.IsDigit(ch) || ch == (char)Keys.Back;
        }

        #endregion

        #region === Controls Helper ===

        private IEnumerable<ListViewItem.ListViewSubItem> GetItemsFromListView(ListView listView)
        {
            foreach (ListViewItem itemRow in listView.Items)
            {
                for (var i = 0; i < itemRow.SubItems.Count; i++)
                {
                    yield return itemRow.SubItems[i];
                }
            }
        }

        #endregion
    }
}
