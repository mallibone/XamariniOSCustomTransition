using UIKit;
using PureLayout.Net;
using CoreGraphics;

namespace CustomTransition
{
    public class MainViewController : UIViewController
    {
        UIButton _button;
        UIButton _button2;

        public UIButton RoundButton => _button;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Custom transition";

            View.BackgroundColor = UIColor.White;

            _button = new UIButton(UIButtonType.System);
            _button.BackgroundColor = UIColor.Purple;
            _button.SetTitleColor(UIColor.White, UIControlState.Normal);
            _button.SetTitle("Gnabber", UIControlState.Normal);
            View.AddSubview(_button);
            _button.AutoCenterInSuperview();

            _button2 = new UIButton(UIButtonType.System);
            _button2.SetTitle("Go to view predefined", UIControlState.Normal);

            View.AddSubview(_button2);
            _button2.BackgroundColor = UIColor.Purple;
            _button2.SetTitleColor(UIColor.White, UIControlState.Normal);
            _button2.AutoAlignAxisToSuperviewAxis(ALAxis.Vertical);
            _button2.AutoPinToBottomLayoutGuideOfViewController(this, 32);

            _button.TouchUpInside += (e, s) =>
            {
                var vc = new ModalViewController(this)
                {
                    ModalPresentationStyle = UIModalPresentationStyle.Custom,
                    TransitioningDelegate = new GrowTransitioningDelegate(_button)
                };

                NavigationController.PresentViewController(vc, true, null);
            };

            _button2.TouchUpInside += (e, s) =>
            {
                var vc = new ModalViewController(this)
                {
                    ModalPresentationStyle = UIModalPresentationStyle.Custom,
                    TransitioningDelegate = new GrowTransitioningDelegate(_button2)
                };
                NavigationController.PresentViewController(vc, true, null);
            };
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _button.Frame = new CGRect(_button.Frame.X, _button.Frame.Y, _button.Frame.Width + 8, _button.Frame.Height);
            _button.Layer.CornerRadius = 5;

            _button2.Frame = new CGRect(_button2.Frame.X, _button2.Frame.Y, _button2.Frame.Width + 8, _button2.Frame.Height);
            _button2.Layer.CornerRadius = 5;
        }
    }
}
