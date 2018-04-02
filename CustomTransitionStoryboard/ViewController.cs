using System;
using Foundation;
using UIKit;

namespace CustomTransitionStoryboard
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
            base.PrepareForSegue(segue, sender);

            var destinationVC = segue.DestinationViewController as SecondViewController;
            destinationVC.Callee = this;
            destinationVC.TransitioningDelegate = new GrowTransitioningDelegate(sender as UIView);
            destinationVC.ModalPresentationStyle = UIModalPresentationStyle.Custom;

            //var customSegue = segue as CustomSegueTransition;
            //customSegue.OriginView = TheButton;
		}

		public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
