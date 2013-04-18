using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sky.Web.Views
{
    public class DefaultView : Page
    {
        protected Label LabelUser;
        protected GridView GridUser;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += (s, e1) => DoLoad();
        }

        void DoLoad()
        {
            if (Session["claim"] == null) { FormsAuthentication.RedirectToLoginPage(); return; }
            if (IsPostBack) return;
            ShowUserDetails();
        }

        void ShowUserDetails()
        {
            LabelUser.Text = "Welcome! Successfully login from Google.";
            var user = Session["user"] as List<Tuple<string, string>>;
            LabelUser.Text = string.Format("Login as : {0} {1}", user.GetValue("first_name"), user.GetValue("last_name"));
            GridUser.DataSource = user;
            GridUser.DataBind();
        }

    }

    public static class UserHelper
    {
        public static string GetValue(this List<Tuple<string, string>> values, string name)
        {
            return (from v in values where v.Item1.Replace(' ', '_').ToLower() == name select v.Item2).SingleOrDefault();
        }
    }
}
