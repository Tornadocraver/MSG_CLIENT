using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MSG_CLIENT
{
    class ColorListBox : ListBox
    {
        public Boolean AutoWordWrap { get; set; } = true;
        public Boolean AutoScroll { get; set; } = true;
        public Color HighlightColor { get; set; } = Color.GreenYellow;
        public List<int> BlockedSelections { get; set; } = new List<int>(new int[] { -1, 0 });

        Boolean Add = false;
        Color fColor = Color.Black;
        Color bColor = Color.White;

        Dictionary<int, ColorListItem> internalItems = new Dictionary<int, ColorListItem>();
        int lastCount = 0;

        public ColorListBox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
        }
        public int AddColorItem(object _item, Color _bColor, Color _fColor)
        {
            Add = true;
            bColor = _bColor;
            fColor = _fColor;
            return Items.Add(_item);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                ColorListItem iniItem = null;
                try
                {
                    iniItem = new ColorListItem(Items[e.Index], e.ForeColor, e.BackColor);
                }
                catch { base.OnDrawItem(e); return; }
                if (!internalItems.ContainsKey(e.Index))
                {
                    if (Add)
                    {
                        iniItem.ForeColor = fColor;
                        iniItem.BackColor = bColor;
                    }
                    internalItems.Add(e.Index, iniItem.Clone());
                }
                else
                    iniItem = internalItems[e.Index];
                Misc.FillRoundedRectangle(e.Graphics, new SolidBrush(iniItem.BackColor), e.Bounds, 5);
                Misc.DrawRoundedRectangle(e.Graphics, ((e.State & DrawItemState.Selected) == DrawItemState.Selected && BlockedSelections.Contains(e.Index) == false) ? new Pen(HighlightColor) : new Pen(new SolidBrush(BackColor)), e.Bounds, 5);
                e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, new SolidBrush(iniItem.ForeColor), e.Bounds);
                e.DrawFocusRectangle();
                iniItem.Dispose();
            }
            else
                base.OnDrawItem(e);
            if (AutoScroll)
                TopIndex = Math.Max(Items.Count - (int)(ClientSize.Height / ItemHeight) + 1, 0);
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (Items.Count != 0)
                e.ItemHeight = (int)e.Graphics.MeasureString(Items[e.Index].ToString(), Font, Width, StringFormat.GenericTypographic).Height;
        }

        #region Old Attempts
        /*
        public string[] SplitByWidth(string _input, Graphics _graph, Font _inFont, int _maxWidth)
        {
            List<string> output = new List<string>();
            String line = string.Empty;
            int TotalWidth = 0;
            char[] chars = _input.ToCharArray();
            foreach (char c in chars)
            {
                //Size s = TextRenderer.MeasureText(c.ToString(), _inFont);
                //MeasureDisplayStringWidth(c.ToString(), _graph, _inFont);
                line = line + c.ToString();
                int width = (int)_graph.MeasureString(line, _inFont, _maxWidth, StringFormat.GenericTypographic).Width;
                TotalWidth = TotalWidth + width;
                if (TotalWidth > _maxWidth)
                {
                    line = line.Replace(c.ToString(), "");
                    output.Add(line);
                    line = c.ToString();
                    TotalWidth = 0;
                }
            }
            if (output.Count >= 1)
            { 
                if (!string.IsNullOrEmpty(line))
                    output.Add(line);
                return output.ToArray();
            }
            else
            {
                if (!string.IsNullOrEmpty(line))
                {
                    output.Add(line);
                    return output.ToArray();
                }
                else
                    return null;
            }
        }

        public int MeasureDisplayStringWidth(string _input, Graphics _graph, Font _font)
        {
            StringFormat format = new StringFormat();
            RectangleF rect = new RectangleF(0, 0, 1000, 1000);
            CharacterRange[] ranges = { new CharacterRange(0, _input.Length) };
            Region[] regions = new Region[1];
            format.SetMeasurableCharacterRanges(ranges);
            regions = _graph.MeasureCharacterRanges(_input, _font, rect, format);
            rect = regions[0].GetBounds(_graph);
            return (int)(rect.Right + 1.0f);
        }
        public int MeasureDisplayStringWidth2(string _input, Graphics _graph, Font _font)
        {
            const int width = 32;
            Bitmap bmp = new Bitmap(width, 1, _graph);
            SizeF size = _graph.MeasureString(_input, _font);
            Graphics g = Graphics.FromImage(bmp);
            int measuredWitdh = (int)size.Width;
            if (g != null)
            {
                g.Clear(Color.White);
                g.DrawString(_input + "|", _font, Brushes.Black, width - measuredWitdh, _font.Height / 2);
                for (int i = measuredWitdh - 1; i >= 0; i--)
                {
                    measuredWitdh--;
                    if (bmp.GetPixel(i, 0).R != 255)
                        break;
                }
            }
            return measuredWitdh;
        }*/
        #endregion
    }
}