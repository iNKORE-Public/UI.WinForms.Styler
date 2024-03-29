﻿using System;
using System.Drawing;

namespace iNKORE.UI.WinForms.Styler.ThemeText {
	public partial class ThemedText : IDisposable {

		public void Draw(Graphics g, RectangleF rect) {
			Draw(g, new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
		}

		public void Draw(Graphics g, Point location, Size size) {
			Draw(g, new Rectangle(location, size));
		}

		public void Draw(Graphics g, PointF location, SizeF size) {
			Draw(g, new Rectangle((int)location.X, (int)location.Y, (int)size.Width, (int)size.Height));
		}

		public void Draw(Graphics g, Rectangle rect) {
			//Convert to native rect
			Native.RECT RECT = new Native.RECT(rect);

			//Get HDC
			IntPtr outputHDC = g.GetHdc();

			//Draw
			Native.GDI.BitBlt(outputHDC, rect.X, rect.Y, rect.Width, rect.Height, _TextHDC, 0, 0, Native.GDI.BitBltOp.SRCCOPY);

			//Clean up
			g.ReleaseHdc(outputHDC);
		}

	}
}
