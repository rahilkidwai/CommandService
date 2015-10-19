using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Command.Service
{
    public partial class Help : System.Web.UI.Page
    {
        private IList<KeyValuePair<string, int>> categories;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            categories = Commands.Instance.GetCommandCategories();
            if (categories == null) return;
            int totalCommands = 0;
            foreach (KeyValuePair<string, int> category in categories) totalCommands += category.Value;
            uxLiteralCategories.Text = GetCategories();

            uxLiteralCount.Text = string.Format("Categories: {0}, Commands: {1}", categories.Count, totalCommands);
        }

        private string GetCategories()
        {
            StringBuilder sb = new StringBuilder();
            if (categories == null || categories.Count == 0)
            {
                sb.Append("No command category found");
            }
            else
            {
                int currCol, maxCol;
                currCol = maxCol = 4;
                foreach (KeyValuePair<string, int> category in categories.OrderBy(cat => cat.Key))
                {
                    if (currCol == maxCol) // start row
                    {
                        sb.Append("<div class='row'>");
                        currCol = 0;
                    }

                    sb.Append("<div class='col-md-3'><h4><a href='").Append(this.ResolveUrl("~/help/")).Append(category.Key).Append("'>").Append(category.Key).Append("</a>&nbsp;(").Append(category.Value).Append(")</h3></div>");
                    ++currCol;

                    if (currCol == 4)
                    {
                        sb.Append("</div>"); //end row
                    }
                }
                for (int i = currCol; i < maxCol; i++)
                {
                    sb.Append("<div class='col-md-4'>&nbsp;</div>");
                }
            }
            return sb.ToString();
        }
    }
}

