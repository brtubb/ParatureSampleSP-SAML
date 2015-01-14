using System;
using System.Web;
using Kentor.AuthServices.Configuration;

namespace SAMLdecoder
{
    public class SessionManager
    {
        public static string Name
        {
            get
            {
                var name = default(string);
                try
                {
                    name = HttpContext.Current.Session["Name"].ToString();
                }
                catch (Exception e) { }

                return name;
            }
            set
            {
                try
                {
                    HttpContext.Current.Session["Name"] = value;
                }
                catch (Exception e) { }
            }
        }

        public static IOptions KentorOptions
        {
            get
            {
                return new Options(KentorAuthServicesSection.Current);
            }
        }
    }
}