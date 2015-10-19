using System;
using System.Collections.Generic;
using System.Text;

namespace Command.Service
{
    public partial class Search : System.Web.UI.Page
    {
        private IList<KeyValuePair<string, int>> categories;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected void uxButtonSearch_Click(object sender, EventArgs e)
        {
            string searchText = uxTextBoxSearch.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(searchText))
            {
                uxLiteralResult.Text = string.Empty;
                return;
            }     
            
        }
    }
}