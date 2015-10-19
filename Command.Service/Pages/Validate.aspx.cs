using Command.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command.Service
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Validate : System.Web.UI.Page
    {
        private List<Lazy<ICommand, ICommandProperties>> _dupes;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _dupes = Commands.Instance.GetUnavailableCommands();
            uxLiteralErrors.Text = GetDetails();
        }

        private string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            if (_dupes == null || _dupes.Count == 0)
            {
                uxLiteralHeader.Text = "<div class='text-success'>No Problem Found.</div>";
            }
            else
            {
                uxLiteralHeader.Text = "<div class='text-danger'>" + _dupes.Count + "&nbsp;Problem(s) Found:</div>";
                int count = 0;
                Type type = null;
                foreach (Lazy<ICommand, ICommandProperties> dupe in _dupes)
                {
                    type = dupe.Value.GetType();
                    sb.Append("<div class='row'><div class='col-md-12'><h4 class='text-warning'>").Append(++count).Append(".&nbsp;").Append(dupe.Metadata.Category).Append(".").Append(dupe.Metadata.Name).Append("</h4></div>");                    
                    sb.Append("<div class='col-md-12'><h6>Type: ").Append(type.FullName).Append("</h6></div>");
                    sb.Append("<div class='col-md-12'><h6>Assembly: ").Append(type.Assembly.FullName).Append("</h6></div></div>");
                }
            }
            return sb.ToString();
        }
    }
}
