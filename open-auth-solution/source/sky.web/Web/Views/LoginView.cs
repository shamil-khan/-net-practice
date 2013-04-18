using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sky.Web.Views
{
    public class LoginView : Page
    {
        protected Label LabelStatus;
        protected Button ButtonLoginGoogle;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += (s, e1) => DoLoad();
            ButtonLoginGoogle.Click += (s, e1) => LoginToGoogle();

        }

        void DoLoad()
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            var response = openid.GetResponse();
            if (response == null) return;
            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    var fetch = response.GetExtension<FetchResponse>();
                    Session["claim"] = response.ClaimedIdentifier;
                    Session["user"] = GetUserDetail(fetch);

                    // This is where you would look for any OpenID extension responses included
                    // in the authentication assertion.
                    //var claimsResponse = response.GetExtension<ClaimsResponse>();
                    //Database.ProfileFields = claimsResponse;
                    // Store off the "friendly" username to display -- NOT for username lookup
                    //Database.FriendlyLoginName = response.FriendlyIdentifierForDisplay;

                    // Use FormsAuthentication to tell ASP.NET that the user is now logged in,
                    // with the OpenID Claimed Identifier as their username.
                    FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier, false);
                    break;
                case AuthenticationStatus.Canceled:
                    break;
                case AuthenticationStatus.Failed:
                    break;

            }
            LabelStatus.Text = string.Format("Status : {0}", response.Status);
        }

        void LoginToGoogle()
        {
            try
            {
                using (OpenIdRelyingParty party = new OpenIdRelyingParty())
                {
                    IAuthenticationRequest request = party.CreateRequest(ConfigurationManager.AppSettings["google-auth-path"]);
                    var fetch = new FetchRequest();
                    fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                    fetch.Attributes.AddRequired(WellKnownAttributes.Name.First);
                    fetch.Attributes.AddRequired(WellKnownAttributes.Name.Last);
                    request.AddExtension(fetch);

                    //request.AddExtension(new ClaimsRequest
                    //{
                    //    Country = DemandLevel.Request,
                    //    Email = DemandLevel.Request,
                    //    Gender = DemandLevel.Require,
                    //    PostalCode = DemandLevel.Require,
                    //    TimeZone = DemandLevel.Require,
                    //});

                    request.RedirectToProvider();
                }
            }
            catch (ProtocolException ex)
            {
                LabelStatus.Text = ex.Message;
            }
        }

        List<Tuple<string, string>> GetUserDetail(FetchResponse fetch)
        {
            List<Tuple<string, string>> items = new List<Tuple<string, string>>();
            items.Add(new Tuple<string, string>("Email", fetch.GetAttributeValue(WellKnownAttributes.Contact.Email)));
            items.Add(new Tuple<string, string>("First Name", fetch.GetAttributeValue(WellKnownAttributes.Name.First)));
            items.Add(new Tuple<string, string>("Last Name", fetch.GetAttributeValue(WellKnownAttributes.Name.Last)));
            return items;
        }

    }
}
