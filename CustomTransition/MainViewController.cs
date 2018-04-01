using UIKit;
using PureLayout.Net;
using CoreGraphics;

namespace CustomTransition
{
    public class MainViewController : UIViewController
    {
        UIButton button;
        public UIButton RoundButton => button;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            button = new UIButton(UIButtonType.System);
            button.BackgroundColor = UIColor.Purple;
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.SetTitle("Gnabber", UIControlState.Normal);
            View.AddSubview(button);
            button.AutoCenterInSuperview();

            var button2 = new UIButton(UIButtonType.System);
            button2.SetTitle("Go to view predefined", UIControlState.Normal);

            View.AddSubview(button2);
            button2.BackgroundColor = UIColor.Purple;
            button2.SetTitleColor(UIColor.White, UIControlState.Normal);
            button2.AutoAlignAxisToSuperviewAxis(ALAxis.Vertical);
            button2.AutoPinToBottomLayoutGuideOfViewController(this, 32);

            button.TouchUpInside += (e, s) =>
            {
                var vc = new ModalViewController(this)
                {
                    ModalPresentationStyle = UIModalPresentationStyle.Custom,
                    TransitioningDelegate = new GrowTransitioningDelegate(button)
                };

                NavigationController.PresentViewController(vc, true, null);
            };

            button2.TouchUpInside += (e, s) =>
            {
                var vc = new ModalViewController(this)
                {
                    ModalPresentationStyle = UIModalPresentationStyle.Custom,
                    TransitioningDelegate = new GrowTransitioningDelegate(button2)
                };
                NavigationController.PresentViewController(vc, true, null);
            };
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            button.Frame = new CGRect(button.Frame.X, button.Frame.Y, button.Frame.Width + 8, button.Frame.Height);
            button.Layer.CornerRadius = 5;
        }
    }
}
