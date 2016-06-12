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

		[Test]
		public void TestRegister()
		{
			try
			{

				var random = (new Random()).Next();
				Console.Out.WriteLine("Key : " + random);

				var model = new Model();

				app.Tap(model.RegisterButton);

				app.WaitForElement(model.RegisterName);

				var email = "Artist" + random + "@email.com";

				app.Tap(model.RegisterEmail);
				app.EnterText(email);

				app.Tap(model.RegisterName);
				app.EnterText("Artist Name " + random);

				app.Tap(model.RegisterPassword);
				app.EnterText("123456");

				app.Tap(model.RegisterPasswordConfirmation);
				app.EnterText("123456");

				app.PressEnter();

				app.Tap(model.RegisterRegisterButton);

				app.WaitForElement(model.PhoneVerificationNumber);

				app.Tap(model.PhoneVerificationNumber);
				app.EnterText(random.ToString());

				app.PressEnter();

				app.Tap(model.PhoneVerificationButton);

				app.WaitForElement(model.PhoneNumberConfirmationSend);

				app.Tap(model.PhoneNumberConfirmationSend);

				app.WaitForElement(model.ToastMessage);

				var code = app.Query(model.ToastMessage).First().Text;

				Console.Out.WriteLine(code);

				code = code.Replace("Code: ", "");

				app.Tap(model.PhoneVerificationCodeNumber);
				app.EnterText(code);

				app.Tap(model.PhoneVerificationCodeButton);

				app.Tap(model.PhoneNumberConfirmationSend);

				app.Tap(model.FollowArtistButton);

				app.Tap(model.FollowDoneButton);

				var wc = new System.Net.WebClient();
				var output = wc.DownloadString("http://zapi.klsandbox.com/data/set-as-artist/" + email);

				Console.WriteLine(output);
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
	}
}

