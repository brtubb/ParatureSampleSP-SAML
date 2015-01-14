using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAMLdecoder
{
    public partial class Status : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageViewState();
        }

        public void RunSso(Object sender, EventArgs e)
        {
            if (SessionManager.Name != null)
            {
                SessionManager.Name = null;
            }
            else
            {
                var url =
                    string.Format(
                        "https://sso.parature.com/idp/startSSO.ping" +
                        "?PartnerSpId={0}", SessionManager.KentorOptions.SPOptions.EntityId.Id);
                Response.Redirect(url);
            }

            ManageViewState();
        }

        private void ManageViewState()
        {
            var name = SessionManager.Name;
            if (name == null)
            {
                AuthStatus.Text = "Unauthenticated";
                Name.Text = string.Empty;
                startSSO.Text = "Start SSO";
            }
            else
            {
                AuthStatus.Text = "Authenticated";
                Name.Text = name;
                startSSO.Text = "Logout";
            }       
        }
    }
}