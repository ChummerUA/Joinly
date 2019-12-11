using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Jointly.Behaviors
{
    public class InputGridBehavior : Behavior<Grid>
    {
        private Frame _frame;
        private Label _label;
        private Grid _grid;

        protected override void OnAttachedTo(Grid grid)
        {
            base.OnAttachedTo(grid);
            _grid = grid;
            _grid.ChildAdded += Grid_ChildAdded;
        }

        private void Grid_ChildAdded(object sender, ElementEventArgs e)
        {
            if (_grid.Children.FirstOrDefault(x => x.GetType() == typeof(Frame)) is Frame frame)
            {
                _frame = frame;
            }
            if (_grid.Children.FirstOrDefault(x => x.GetType() == typeof(Label)) is Label label)
            {
                _label = label;
            }
            if (_label != null && _frame != null)
            {
                _grid.ChildAdded -= Grid_ChildAdded;
                _frame.SizeChanged += Frame_SizeChanged;
                FixChildren();
            }
        }

        protected override void OnDetachingFrom(Grid grid)
        {
            base.OnDetachingFrom(grid);
            _frame.SizeChanged -= Frame_SizeChanged;
            _grid.ChildAdded -= Grid_ChildAdded;
        }

        private void Frame_SizeChanged(object sender, EventArgs e)
        {
            FixChildren();
        }

        private void FixChildren()
        {
            float corner = 0;
            if (_frame.Content is Editor editor)
                corner = 20;
            else
                corner = (float)_frame.Height / 2;

            if(corner > 0)
            {
                _frame.CornerRadius = corner;
                _frame.Padding = new Thickness(_frame.CornerRadius / 2, 0);
                _label.Margin = _frame.Padding;
            }
        }
    }
}
