using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;

using System.Runtime.CompilerServices;
using System.Threading;

namespace GeoHypernation.DesignHelpers
{
    public class RoundedCheckBox : CheckBox
    {
        private Color checkboxCheckColor;
        private Color checkboxColor;
        private Color checkboxHoverColor;
        private Style checkboxStyle;
        [CompilerGenerated]
        private EventHandler checkedStateChanged;
        private Color currentColor;
        private bool isChecked;
        private int tickThickness;

        // Events
        public event EventHandler CheckedStateChanged
        {
            [CompilerGenerated]
            add
            {
                EventHandler handler2;
                EventHandler checkedStateChanged = this.checkedStateChanged;
                do
                {
                    handler2 = checkedStateChanged;
                    EventHandler handler3 = (EventHandler)Delegate.Combine(handler2, value);
                    checkedStateChanged = Interlocked.CompareExchange<EventHandler>(ref this.checkedStateChanged, handler3, handler2);
                }
                while (checkedStateChanged != handler2);
            }
            [CompilerGenerated]
            remove
            {
                EventHandler handler2;
                EventHandler checkedStateChanged = this.checkedStateChanged;
                do
                {
                    handler2 = checkedStateChanged;
                    EventHandler handler3 = (EventHandler)Delegate.Remove(handler2, value);
                    checkedStateChanged = Interlocked.CompareExchange<EventHandler>(ref this.checkedStateChanged, handler3, handler2);
                }
                while (checkedStateChanged != handler2);
            }
        }


        // Methods
        public RoundedCheckBox()
        {
            this.tickThickness = 2;
            this.checkboxColor = Color.FromArgb(0, 0xa2, 250);
            this.checkboxCheckColor = Color.White;
            this.checkboxHoverColor = Color.FromArgb(0xf9, 0x37, 0x62);
            this.checkboxStyle = Style.Material;
            base.Size = new Size(100, 20);
            this.Text = base.Name;
            this.ForeColor = Color.White;
            this.currentColor = this.checkboxColor;
        }

        protected virtual void OnCheckedStateChanged()
        {
            if (this.checkedStateChanged != null)
            {
                this.checkedStateChanged(this, new EventArgs());
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this.Checked)
            {
                this.Checked = true;
            }
            else
            {
                this.Checked = false;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.currentColor = this.checkboxHoverColor;
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.currentColor = this.checkboxColor;
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.checkboxStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(this.currentColor), 1, 1, base.Height - 2, base.Height - 2);
                if (this.isChecked)
                {
                    e.Graphics.DrawLine(new Pen(this.checkboxCheckColor, (float)this.tickThickness), 2, (base.Height / 3) * 2, base.Height / 2, base.Height - 2);
                    e.Graphics.DrawLine(new Pen(this.checkboxCheckColor, (float)this.tickThickness), base.Height / 2, base.Height - 2, base.Height - 2, 1);
                }
            }
            if (this.checkboxStyle == Style.iOS)
            {
                if (!this.isChecked)
                {
                    e.Graphics.DrawEllipse(new Pen(Color.FromArgb(200, 200, 200)), 2, 2, base.Height - 4, base.Height - 4);
                }
                if (this.isChecked)
                {
                    e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 120, 0xff)), 1, 1, base.Height - 2, base.Height - 2);
                    e.Graphics.DrawLine(new Pen(Color.White, (float)this.tickThickness), (int)(base.Height / 5), (int)(base.Height / 2), (int)(base.Height / 2), (int)((base.Height / 4) * 3));
                    e.Graphics.DrawLine(new Pen(Color.White, (float)this.tickThickness), (int)(base.Height / 2), (int)((base.Height / 4) * 3), (int)((base.Height / 5) * 4), (int)(base.Height / 4));
                }
            }
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Near;
            SolidBrush brush = new SolidBrush(this.ForeColor);
            RectangleF layoutRectangle = new RectangleF((float)(base.Height + 3), 0f, (float)((base.Width - base.Height) - 2), (float)base.Height);
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.DrawString(this.Text, this.Font, brush, layoutRectangle, format);
            base.OnPaint(e);
        }

        // Properties
        public Color CheckboxCheckColor
        {
            get
            {
                return this.checkboxCheckColor;
            }
            set
            {
                this.checkboxCheckColor = value;
                base.Invalidate();
            }
        }

        public Color CheckboxColor
        {
            get
            {
                return this.checkboxColor;
            }
            set
            {
                this.checkboxColor = value;
                this.currentColor = value;
                base.Invalidate();
            }
        }

        public Color CheckboxHoverColor
        {
            get
            {
                return this.checkboxHoverColor;
            }
            set
            {
                this.checkboxHoverColor = value;
                base.Invalidate();
            }
        }

        public Style CheckboxStyle
        {
            get
            {
                return this.checkboxStyle;
            }
            set
            {
                this.checkboxStyle = value;
                base.Invalidate();
            }
        }

        public bool Checked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                this.isChecked = value;
                this.OnCheckedStateChanged();
                base.Invalidate();
            }
        }

        public int TickThickness
        {
            get
            {
                return this.tickThickness;
            }
            set
            {
                this.tickThickness = value;
                base.Invalidate();
            }
        }
        // Nested Types
        public enum Style
        {
            iOS,
            Material
        }
    }
}
