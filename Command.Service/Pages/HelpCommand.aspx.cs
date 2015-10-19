using Command.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Command.Service
{
    public partial class HelpCommand : System.Web.UI.Page
    {
        private string category;
        private string name;
        private ICommandProperties metadata;
        private CommandDocumentation doc;

        protected override void OnLoad(EventArgs e)
        {   
            base.OnLoad(e);
            GetDocumentation();
            if (doc == null) return;
            uxLiteralCommand.Text = string.Format("<a href='{0}{1}'>{1}</a>.{2}", this.ResolveUrl("~/help/"), metadata.Category, metadata.Name);
            uxLiteralDescription.Text = doc.Description;
            uxLiteralType.Text = doc.RelatedType == null ? "-" : doc.RelatedType.FullName;
            uxLiteralAssembly.Text = doc.RelatedType == null ? "-" :  doc.RelatedType.Assembly.FullName;
            uxLiteralParameters.Text = SetDocumentation();
            uxLiteralReturn.Text = GetReturnTypeDocumentation();
        }

        private string GetReturnTypeDocumentation()
        {
            if (doc != null && doc.ReturnTypeDocumentation != null)
            {
                StringBuilder sb = new StringBuilder();
                if(!doc.ReturnTypeDocumentation.Type.IsGenericType)
                {
                    sb.Append("<a href='http://msdn.microsoft.com/en-us/library/").Append(doc.ReturnTypeDocumentation.Type.FullName).Append("'>").Append(doc.ReturnTypeDocumentation.Type.Name).Append("</a>");
                }
                else
                {
                    sb.Append(GetTypeName(doc.ReturnTypeDocumentation.Type));
                }
                sb.Append("<br/>").Append(doc.ReturnTypeDocumentation.Description);
                return sb.ToString();
            }
            return "-";
        }

        private string GetTypeName(Type t)
        {
            if (!t.IsGenericType) return t.Name;
            string genericTypeName = t.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
            string genericArgs = string.Join(",", t.GetGenericArguments().Select(ta => GetTypeName(ta)).ToArray());
            return string.Format("{0}&lt;{1}&gt;", genericTypeName, genericArgs);
        }

        private string SetDocumentation()
        {
            StringBuilder sb = new StringBuilder();
            if(doc == null || doc.ParametersDocumentation == null || doc.ParametersDocumentation.Count == 0)
            {
                sb.Append("None");
            }
            else
            {
                sb.Append("<table class='table table-striped table-hover'><thead><tr class='active'><th>Name</th><th>Type</th><th>Description</th></tr></thead><tbody>");
                foreach (CommandParameterDocumentation parameter in doc.ParametersDocumentation)
                {
                    sb.Append("<tr><td>").Append(parameter.Name).Append("</td><td><a href='http://msdn.microsoft.com/en-us/library/").Append(parameter.Type.FullName).Append("'>").Append(GetTypeName(parameter.Type)).Append("</a></td><td>").Append(parameter.Description).Append("</td></tr>");
                }
                sb.Append("</tbody></table>");
            }
            return sb.ToString();
        }

        private void GetDocumentation()
        {
            category = (string)Context.Items["commandcategory"];
            name = (string)Context.Items["commandname"];
            if (category != null && name != null)
            {
                KeyValuePair<ICommandProperties, CommandDocumentation> pair = Commands.Instance.GetCommandDocumentation(category, name);
                metadata = pair.Key;
                doc = pair.Value;
            }
        }
    }
}

