using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;

namespace Credit.Work.Credit
{
    public class NotCanScrollGridLayoutManager : GridLayoutManager
    {
        public override bool CanScrollVertically()
        {
            return false;
        }

        protected NotCanScrollGridLayoutManager(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,
            transfer)
        {
        }

        public NotCanScrollGridLayoutManager(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) :
            base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public NotCanScrollGridLayoutManager(Context context, int spanCount) : base(context, spanCount)
        {
        }

        public NotCanScrollGridLayoutManager(Context context, int spanCount, int orientation, bool reverseLayout) :
            base(context, spanCount, orientation, reverseLayout)
        {
        }
    }
}