using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Command.Business;

namespace Command.Service
{
    public partial class HelpCategory : System.Web.UI.Page
    {
        private string category;
        private IList<KeyValuePair<ICommandProperties, CommandDocumentation>> commands;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GetDocumentation();
            if (commands == null) return;

            uxLiteralCategory.Text = string.Format("{0}&nbsp;<a href='{1}'>Commands</a>&nbsp;({2})", commands[0].Key.Category, this.ResolveUrl("~/help"), commands.Count);
            uxLiteralCommands.Text = SetCommandsDocumentation();
        }

        private string SetCommandsDocumentation()
        {
            StringBuilder sb = new StringBuilder();
            if (commands == null || commands.Count == 0)
            {
                sb.Append("None");
            }
            else
            {
                sb.Append("<table class='table table-striped table-hover'><thead><tr class='active'><th>Name</th><th>Description</th></tr></thead><tbody>");
                foreach (KeyValuePair<ICommandProperties, CommandDocumentation> command in commands.OrderBy(cmd => cmd.Key.Name))
                {
                    sb.Append("<tr><td><a href='").Append(this.ResolveUrl("~/help/")).Append(command.Key.Category).Append("/").Append(command.Key.Name).Append("'>").Append(command.Key.Name).Append("</a></td><td>").Append(command.Value.Description).Append("</td></tr>");
                }
                sb.Append("</tbody></table>");
            }
            return sb.ToString();
        }

        private void GetDocumentation()
        {
            category = (string)Context.Items["commandcategory"];
            if (category != null)
            {
                commands = Commands.Instance.GetCommandsDocumentation(category);
            }
        }
    }
}

