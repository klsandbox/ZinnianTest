using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;
using QQ = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ZinnianTest
{
	public class Model
	{
		private QQ marked(string name)
		{
			return q => q.Marked(name);
		}

		public QQ EmailLogin
		{
			get
			{
				return marked("al_btn_email_login");
			}
		}

		public QQ RegisterButton
		{
			get
			{
				return marked("al_btn_register");
			}
		}

		public QQ RegisterPassword
		{
			get
			{
				return marked("ar_input_password");
			}
		}

		public QQ RegisterPasswordConfirmation
		{
			get
			{
				return marked("ar_input_passwordConfirmation");
			}
		}


		public QQ RegisterName
		{
			get
			{
				return marked("ar_input_name");
			}
		}

		public QQ RegisterEmail
		{
			get
			{
				return marked("ar_input_email");
			}
		}

		public QQ RegisterRegisterButton
		{
			get
			{
				return marked("ar_btn_register");
			}
		}

		public QQ PhoneVerificationNumber
		{
			get
			{
				return marked("pv_input_editTxt");
			}
		}

		public QQ PhoneVerificationButton
		{
			get
			{
				return marked("pv_verify_btn");
			}
		}

		public QQ PhoneVerificationCodeNumber
		{
			get
			{
				return this.PhoneVerificationNumber;
			}
		}
		public QQ PhoneVerificationCodeButton
		{
			get
			{
				return this.PhoneVerificationButton;
			}
		}


		public QQ PhoneNumberConfirmationSend
		{
			get
			{
				return marked("button1");
			}
		}

		public QQ ToastMessage
		{
			get
			{
				return marked("message");
			}
		}

		public QQ FollowArtistButton
		{
			get
			{
				return q => q.Marked("follow_button_follow").Index(0);
			}
		}

		public QQ FollowDoneButton
		{
			get
			{
				return marked("follow_btn_done");
			}
		}
	}
}