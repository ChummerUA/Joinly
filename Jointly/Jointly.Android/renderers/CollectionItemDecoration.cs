using Android.Graphics;
using AndroidX.RecyclerView.Widget;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;
using ItemViewType = Xamarin.Forms.Platform.Android.ItemViewType;

namespace Jointly.Droid.Renderers
{
    public class CollectionItemDecoration : SpacingItemDecoration
    {
        readonly ItemsLayoutOrientation _orientation;
        readonly double _verticalSpacing;
        double _adjustedVerticalSpacing = -1;
        double _adjustedVerticalOffset;
        readonly double _horizontalSpacing;
        double _adjustedHorizontalSpacing = -1;
        readonly double _adjustedHorizontalOffset;
        readonly int _spanCount;
        readonly Rect _offset;

        public CollectionItemDecoration(IItemsLayout itemsLayout, Rect offset) : base(itemsLayout)
        {
            if (itemsLayout == null)
            {
                throw new ArgumentNullException(nameof(itemsLayout));
            }

            _offset = offset;

            switch (itemsLayout)
            {
                case GridItemsLayout gridItemsLayout:
                    _orientation = gridItemsLayout.Orientation;
                    _horizontalSpacing = gridItemsLayout.HorizontalItemSpacing;
                    _verticalSpacing = gridItemsLayout.VerticalItemSpacing;
                    _spanCount = gridItemsLayout.Span;
                    break;
                case LinearItemsLayout listItemsLayout:
                    _orientation = listItemsLayout.Orientation;
                    if (_orientation == ItemsLayoutOrientation.Horizontal)
                        _horizontalSpacing = listItemsLayout.ItemSpacing;
                    else
                        _verticalSpacing = listItemsLayout.ItemSpacing;
                    _spanCount = 1;
                    break;
            }
        }

        public override void GetItemOffsets(Rect outRect, AView view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);

            if (_adjustedVerticalSpacing == -1)
            {
                _adjustedVerticalSpacing = parent.Context.ToPixels(_verticalSpacing);
            }

            if (_adjustedHorizontalSpacing == -1)
            {
                _adjustedHorizontalSpacing = parent.Context.ToPixels(_horizontalSpacing);
            }

            var itemViewType = parent.GetChildViewHolder(view).ItemViewType;

            if (itemViewType == ItemViewType.Header)
            {
                outRect.Top = _offset.Top;
                outRect.Left = _offset.Left;
                outRect.Right = outRect.Right;
                outRect.Bottom = (int)_adjustedVerticalSpacing;
                return;
            }

            if (itemViewType == ItemViewType.Footer)
            {
                outRect.Bottom = _offset.Bottom;
                outRect.Left = _offset.Left;
                outRect.Right = outRect.Right;
                outRect.Top = (int)_adjustedVerticalSpacing;
                return;
            }

            var spanIndex = 0;

            if (view.LayoutParameters is GridLayoutManager.LayoutParams gridLayoutParameters)
            {
                spanIndex = gridLayoutParameters.SpanIndex;
            }

            var adapter = parent.GetAdapter();
            var childCount = adapter.ItemCount - 1;
            var index = parent.GetChildAdapterPosition(view);
            var lastSpanIndex = childCount - (_spanCount - childCount % _spanCount);

            if (_orientation == ItemsLayoutOrientation.Vertical)
            {
                outRect.Left = index < _spanCount ? _offset.Left : (int)_adjustedHorizontalOffset;
                if (_spanCount > 1)
                    outRect.Right = index < lastSpanIndex ? (int)_adjustedHorizontalOffset : _offset.Right;
                else
                    outRect.Right = index < childCount ? (int)_adjustedHorizontalOffset : _offset.Right;
                outRect.Top = spanIndex == 0 ? _offset.Top : (int)_adjustedVerticalOffset;
                outRect.Bottom = spanIndex == _spanCount - 1 ? _offset.Bottom : (int)_adjustedVerticalOffset;
            }

            if (_orientation == ItemsLayoutOrientation.Horizontal)
            {
                outRect.Left = spanIndex == 0 ? _offset.Left : (int)_adjustedHorizontalOffset;
                outRect.Right = spanIndex == _spanCount - 1 ? _offset.Right : (int)_adjustedHorizontalOffset;
                outRect.Top = index < _spanCount ? _offset.Top : (int)_adjustedVerticalOffset;
                if (_spanCount > 1)
                    outRect.Bottom = index < lastSpanIndex ? (int)_adjustedVerticalOffset : _offset.Bottom;
                else
                    outRect.Bottom = index < childCount ? (int)_adjustedVerticalOffset : _offset.Bottom;
            }
        }
    }
}