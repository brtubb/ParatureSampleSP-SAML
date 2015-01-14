using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using Kentor.AuthServices.Configuration;
using Kentor.AuthServices.WebSso;

/*
 * How To:
 * 1. Set up Portal as an IdP using the Intranet. (Must be performed by a Parature Technician)
 * 2. (This is performed by a Parature Technician) Set the :
 *  2a. ACS Url to the url of this website. This can be changed later. During testing it will likely be a localhost address
 *  2b. SP Id - Unique ID for this sso instance (department specific). 
 *      Likely never changes. Needs to be formatted as a URI, but is not used at all except in the below link
 * 3. Trigger an IdP initiated SSO:
 *  https://sso.parature.com/idp/startSSO.ping?PartnerSpId={YOUR-SP-NAME-IN-PING}
 *      Where {YOUR-SP-NAME-IN-PING} is the "EntityId" in the webconfig, must be globally unique in our system, and is purely an id (not actually used)
 *      
 * This will kick off IdP initiated SSO and end at the SPSiteUrl.
 */
namespace SAMLdecoder
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        { }

        void Application_End(object sender, EventArgs e)
        { }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            /*
             * This pulls in the options specified in the kentor.authServices section in the web config
             * Errors are pretty clearly indicated with exceptions. Common issues:
             *  - EntityId of the SP doesn't match (in Parature's system and kentor.authServices[entityId] in webconfig)
             *  - Missing Parature public certificate and/or misconfigured signingCertificate section
             */
            var samlVal = Request["SAMLResponse"];
            if (samlVal != null)
            {
                var options = SessionManager.KentorOptions;
                KentorAuthServicesSection.Current.IdentityProviders.RegisterIdentityProviders(options);
                KentorAuthServicesSection.Current.Federations.RegisterFederations(options);
                var req = new HttpRequestWrapper(Context.Request);
                var reqData = new HttpRequestData(req);
                var samlResp = CommandFactory.GetCommand(CommandFactory.AcsCommandName)
                    .Run(reqData, options);
                var claims = samlResp.Principal.Claims
                    .Where(c => c.Type != "permissions") //permissions has multiple keys/values and won't work in a dictionary
                    .ToDictionary(c => c.Type, c => c.Value);

                //Retrieve name from the claims (portal claims). There is more information available in the claims info.
                SessionManager.Name = claims["firstname"] + " " + claims["lastname"];
                Response.Redirect(options.SPOptions.ReturnUrl.ToString());
            }
        }

        void Application_Error(object sender, EventArgs e)
        { }
    }
}
