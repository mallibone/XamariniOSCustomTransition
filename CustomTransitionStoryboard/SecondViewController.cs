using Foundation;
using System;
using UIKit;

namespace CustomTransitionStoryboard
{
    public partial class SecondViewController : UIViewController
    {
        public ViewController Callee { get; set; }

        public SecondViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewWillAppear(bool animated)
		{
            base.ViewWillAppear(animated);
            GoBackButton.TouchUpInside += GoBack;
		}

		public override void ViewDidDisappear(bool animated)
		{
            base.ViewDidDisappear(animated);
            GoBackButton.TouchUpInside -= GoBack;
		}

		private void GoBack(object sender, EventArgs e)
        {
            Callee?.DismissViewController(animated: true, completionHandler: null);
        }
	}
}