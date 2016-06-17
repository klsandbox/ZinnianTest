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
	[TestFixture]
	public class Tests
	{
		AndroidApp app;

		[SetUp]
		public void BeforeEachTest()
		{
			// TODO: If the Android app being tested is included in the solution then open
			// the Unit Tests window, right click Test Apps, select Add App Project
			// and select the app projects that should be tested.
			app = ConfigureApp
				.Android
				// TODO: Update this path to point to your Android app and uncomment the
				// code if the app is not included in the solution.
				.ApkFile("/Users/klsandbox/Klsandbox/ZDev/app/build/outputs/apk/app-debug.apk")
				.EnableLocalScreenshots()
				.StartApp();
		}

//		[Test]
		public void TestFeed()
		{
			try
			{
				var randomArtist = (new Random()).Next();
				Console.Out.WriteLine("Key : " + randomArtist);

				var model = new Model();

				//				LoginAsUser("b@b.com", "b");
				app.Tap(c => c.Marked("el_btn_email_login"));

				app.Tap(c => c.Marked("el_input_email"));
				app.EnterText("b@b.com");

				app.Tap(c => c.Marked("el_input_password"));
				app.EnterText("b");

				app.PressEnter();

				app.Tap(c => c.Marked("el_btn_email_login"));

				app.WaitForElement(c => c.Marked("follow_artist_list"));

				var name = "Miss Destiny Rogahn";
				QQ qq = model.FollowArtistButton(name);
				app.Tap(qq);

				this.all();
			}
			catch
			{
				this.all();

				throw;
			}
		}

		[Test]
		public void TestFullScenario()
		{
			try
			{
				var randomArtist = (new Random()).Next();
				Console.Out.WriteLine("Key : " + randomArtist);

				var model = new Model();
				string email = RegisterUser(randomArtist, model);

				var wc = new System.Net.WebClient();
				var output = wc.DownloadString("http://zapi.klsandbox.com/data/set-as-artist/" + email);

				Console.WriteLine(output);

				var randomUser = (new Random()).Next();
				var userEmail = RegisterUser(randomUser, model, getArtistName(randomArtist));
			}
			catch
			{
				this.all();

				throw;
			}
		}

//		[Test]
		public void TestRegister()
		{
			try
			{
				var randomArtist = (new Random()).Next();
				Console.Out.WriteLine("Key : " + randomArtist);

				var model = new Model();
				string email = RegisterUser(randomArtist, model);
			}
			catch
			{
				this.all();

				throw;
			}
		}

		public string[] all()
		{
			var qr = app.Query(c => c.All()).Where(r).Select(this.res).OrderBy(e => e).ToArray();

			var text = app.Query(c => c.All()).Where(e => !string.IsNullOrEmpty(e.Text)).Select(this.res).OrderBy(e => e).ToArray();

			Console.Out.WriteLine("elements " + qr.Length + " text " + text.Length);

			return qr;
		}

		public bool r(AppResult ar)
		{
			return !string.IsNullOrEmpty(ar.Id) || !string.IsNullOrEmpty(ar.Text);
		}

		public string res(AppResult ar)
		{
			return string.Format("{0} Class:{1} Text:{2}", ar.Id, ar.Class, ar.Text);
		}

		//	[Test]
		public void AppLaunches()
		{
			var model = new Model();
			app.Screenshot("First screen.");
			//			var all = app.Query (c => c.All ());
			//
			//			var ff = app.Query ();

			var loginButton = app.Query(model.EmailLogin);

			var text = loginButton.First().Text;

			//			app.Flash(c=>c.Marked("al_btn_email_login"));

			app.Tap(c => c.Marked("al_btn_email_login"));

			app.Tap(c => c.Marked("al_input_email"));
			app.EnterText("b@b.com");

			app.Tap(c => c.Marked("al_input_password"));
			app.EnterText("b");

			app.PressEnter();

			app.Tap(c => c.Marked("al_btn_email_login"));

			app.WaitForElement(c => c.Marked("follow_artist_list"));
			//			app.Repl();
			app.Screenshot("Logged in.");

			app.Flash((c => c.Marked("follow_button_follow").Index(0)));

			app.Tap(c => c.Marked("follow_button_follow").Index(0));

			app.Tap(c => c.Marked("follow_btn_done"));
		}

		string getArtistName(int random)
		{
			return "Artist Name " + random;
		}

		string RegisterUser(int random, Model model, string followUserName = null)
		{
			app.Tap(model.RegisterButton);

			app.WaitForElement(model.RegisterName);

			var email = "Artist" + random + "@email.com";

			app.Tap(model.RegisterEmail);
			app.EnterText(email);

			app.Tap(model.RegisterName);
			app.EnterText(getArtistName(random));

			app.Tap(model.RegisterPassword);
			app.EnterText("123456");

			app.Tap(model.RegisterPasswordConfirmation);
			app.EnterText("123456");

			app.PressEnter();

			app.Tap(model.RegisterRegisterButton);

			app.WaitForElement(model.PhoneVerificationNumberOne);

			app.Tap(model.PhoneVerificationNumberOne);
			app.EnterText("011");

			var fourDigit = String.Format("{0:0000}", random % 10000);

			app.Tap(model.PhoneVerificationNumberTwo);
			app.EnterText("1111");

			app.Tap(model.PhoneVerificationNumberThree);
			app.EnterText(fourDigit);

			app.PressEnter();

			app.Tap(model.PhoneVerificationButton);

			app.WaitForElement(model.PhoneNumberConfirmationSend);

			app.Tap(model.PhoneNumberConfirmationSend);

//			app.WaitForElement(model.ToastMessage);

//			var code = app.Query(model.ToastMessage).First().Text;

//			Console.Out.WriteLine(code);

//			code = code.Replace("Code: ", "");

			app.Tap(model.PhoneVerificationCodeNumber);

			// app.EnterText(code);
			app.EnterText("0000");

			app.Tap(model.PhoneVerificationCodeButton);

			app.Tap(model.PhoneNumberConfirmationSend);

			if (followUserName == null)
			{
				app.Tap(model.FirstFollowArtistButton);
			}
			else
			{
				app.ScrollDownTo(model.FollowArtistButton(followUserName));
				app.Tap(model.FollowArtistButton(followUserName));
			}

			app.Tap(model.FollowDoneButton);

			app.Tap(model.SideMenuHamburger);

			app.Tap(model.SideMenuSetting);

			app.ScrollToVerticalEnd();

			app.Tap(model.SettingsLogout);

			return email;
		}
	}
}

