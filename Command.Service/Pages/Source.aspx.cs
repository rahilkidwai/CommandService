using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

namespace Command.Service
{
    public partial class Source : System.Web.UI.Page
    {
        Assembly[] sources;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            sources = Commands.Instance.GetSources();
            uxLiteralSources.Text = GetSources();
        }

        private string GetSources()
        {
            StringBuilder sb = new StringBuilder();
            if (sources == null || sources.Length == 0)
            {
                sb.Append("No command source found.");
            }
            else
            {
                int count = 0;
                TargetFrameworkAttribute attribute = null;
                foreach (Assembly source in sources)
                {
                    sb.Append("<div class='row'>");
                    sb.Append("<div class='col-md-12'><h4 class='text-success'>").Append(++count).Append(".&nbsp;").Append(source.FullName).Append("</h4>");
                    sb.Append("<ul><li>Location:&nbsp;").Append(source.Location).Append("</li>");
                    object[] attributes = source.GetCustomAttributes(typeof(TargetFrameworkAttribute), false);
                    if(attributes.Length > 0)
                    {
                        attribute = attributes[0] as TargetFrameworkAttribute;
                        sb.Append("<li>Compiled against:&nbsp;").Append(attribute.FrameworkDisplayName).Append("</li>");
                    }                    
                    sb.Append("</ul></div>");
                    sb.Append("</div>");
                }
            }
            return sb.ToString();            
        }
    }
}

