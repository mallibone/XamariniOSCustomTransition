using CoreGraphics;
using UIKit;

namespace CustomTransition
{
    public class GrowTransitioningDelegate : UIViewControllerTransitioningDelegate
    {
        readonly UIView _animationOrigin;

        public GrowTransitioningDelegate(UIView animationOrigin)
        {
            _animationOrigin = animationOrigin;
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
        {
            var customTransition = new GrowTransitionAnimator(_animationOrigin);
            return customTransition;
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController(UIViewController dismissed)
        {
            var customTransition = new ShrinkTransitionAnimator(_animationOrigin);
            return customTransition;
        }
    }

    public class GrowTransitionAnimator : UIViewControllerAnimatedTransitioning
    {
        readonly UIView _animationOrigin;

        public GrowTransitionAnimator(UIView animationOrigin)
        {
            _animationOrigin = animationOrigin;
        }

        public override async void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            // Get the from and to View Controllers and their views
            var fromVC = transitionContext.GetViewControllerForKey(UITransitionContext.FromViewControllerKey);
            var fromView = fromVC.View;

            var toVC = transitionContext.GetViewControllerForKey(UITransitionContext.ToViewControllerKey);
            var toView = toVC.View;

            // Add the to view to the transition container view
            var containerView = transitionContext.ContainerView;
            containerView.AddSubview(toView);


            // Set the desired target for the transition
            var appearedFrame = transitionContext.GetFinalFrameForViewController(toVC);

            // Set how the animation shall start
            var initialFrame = new CGRect(_animationOrigin.Frame.GetMidX(), _animationOrigin.Frame.GetMidY(), 0, 0);
            var finalFrame = appearedFrame;
            toView.Frame = initialFrame;

            var isAnimationCompleted = await UIView.AnimateAsync(TransitionDuration(transitionContext), () => {
                toView.Frame = finalFrame;
            });

            transitionContext.CompleteTransition(isAnimationCompleted);
        }

        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return 0.3;
        }
    }

    public class ShrinkTransitionAnimator : UIViewControllerAnimatedTransitioning
    {
        readonly UIView _animationOrigin;

        public ShrinkTransitionAnimator(UIView animationTarget)
        {
            _animationOrigin = animationTarget;
        }

        public override async void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            // Get the from and to View Controllers and their views
            var fromVC = transitionContext.GetViewControllerForKey(UITransitionContext.FromViewControllerKey);
            var fromView = fromVC.View;

            var toVC = transitionContext.GetViewControllerForKey(UITransitionContext.ToViewControllerKey);
            var toView = toVC.View;

            // Add the to view to the transition container view
            var containerView = transitionContext.ContainerView;

            // Set the desired target for the transition
            var appearedFrame = transitionContext.GetFinalFrameForViewController(fromVC);

            // Set how the animation shall end
            var finalFrame = new CGRect(_animationOrigin.Frame.GetMidX(), _animationOrigin.Frame.GetMidY(), 0, 0);
            fromView.Frame = appearedFrame;

            var isAnimationCompleted = await UIView.AnimateAsync(TransitionDuration(transitionContext), () => {
                fromView.Frame = finalFrame;
            });

            fromView.RemoveFromSuperview();

            transitionContext.CompleteTransition(isAnimationCompleted);
        }

        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return 0.3;
        }
    }

}
