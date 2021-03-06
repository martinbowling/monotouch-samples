
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Security;

namespace Keychain
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var rec = new SecRecord (SecKind.GenericPassword){
				Generic = NSData.FromString ("foo")
			};
			
			SecStatusCode res;
			var match = SecKeyChain.QueryAsRecord (rec, out res);
			if (res == SecStatusCode.Success)
				Console.WriteLine ("Key existed, the password is: {0}", match.ValueData.ToString ());
			else
				Console.WriteLine ("Key not found, code: {0}", res);
			
			
			var s = new SecRecord (SecKind.GenericPassword) {
				Label = "Item Label",
				Description = "Item description",
				Account = "Account",
				Service = "Service",
				Comment = "Your comment here",
				ValueData = NSData.FromString ("my-secret-password"),
				Generic = NSData.FromString ("foo")
			};
			
			var err = SecKeyChain.Add (s);
			
			window.MakeKeyAndVisible ();
			
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
	}
}

