using PureLayout.Net;
using UIKit;

namespace CustomTransition
{
    public class ModalViewController : UIViewController
    {
        private readonly UIViewController _callee;
        public ModalViewController(UIViewController callee)
        {
            _callee = callee;
        }

        private UIButton _button;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Purple;

            _button = new UIButton(UIButtonType.System);
            _button.SetTitle("Go back", UIControlState.Normal);
            _button.SetTitleColor(UIColor.White, UIControlState.Normal);


            _button.TouchUpInside += (e, s) => _callee.DismissViewController(true, () => { });
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            View.AddSubview(_button);
            _button.AutoCenterInSuperview();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _button.RemoveFromSuperview();
        }
    }
}
