using System;
using System.Drawing;
using Microsoft.Win32.SafeHandles;

namespace MSG_CLIENT
{
    class ColorListItem : IDisposable
    {
        public Object Value { get; set; } = null;
        public Color ForeColor { get; set; } = Color.Black;
        public Color BackColor { get; set; } = Color.White;
        private Boolean disposed = false;
        private SafeFileHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public ColorListItem() { }
        public ColorListItem(object _value, Color _fColor, Color _bColor)
        {
            Value = _value;
            ForeColor = _fColor;
            BackColor = _bColor;
        }

        public ColorListItem Clone()
        {
            return new ColorListItem(Value, ForeColor, BackColor);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }
    }
}
