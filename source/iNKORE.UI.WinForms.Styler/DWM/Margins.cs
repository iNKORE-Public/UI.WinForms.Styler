﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace iNKORE.UI.WinForms.Styler.Dwm {

    /// <summary>Margins structure for the Glass Sheet effect.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins {
        public Margins(int left, int right, int top, int bottom) {
            _left = left;
            _right = right;
            _top = top;
            _bottom = bottom;
        }

        public Margins(int allMargins) {
            _left = allMargins;
            _right = allMargins;
            _top = allMargins;
            _bottom = allMargins;
        }

        private int _left;
		private int _right;
		private int _top;
		private int _bottom;

		public int Left { get { return _left; } set { _left = value; } }
		public int Right { get { return _right; } set { _right = value; } }
		public int Top { get { return _top; } set { _top = value; } }
		public int Bottom { get { return _bottom; } set { _bottom = value; } }

        /// <summary>
        /// Gets whether this margin represents a negative measure on each side.
        /// </summary>
        /// <remarks>
        /// Used to specify non-existing margins on glass frames.
        /// </remarks>
		public bool IsMarginless {
			get {
				return (
					_left < 0 && _right < 0 &&
					_top < 0 && _bottom < 0
				);
			}
		}

        /// <summary>
        /// Gets whether this margin measures 0 pixels on each side.
        /// </summary>
		public bool IsNull {
			get {
				return (
					_left == 0 && _right == 0 &&
					_top == 0 && _bottom == 0
				);
			}
		}

        /// <summary>
        /// Returns whether a point in client coordinates is outside the margins defined by this instance.
        /// </summary>
        /// <param name="clientPoint">Point in client coordinates.</param>
        public bool IsOutsideMargins(Point clientPoint, Size clientSize) {
            if (IsMarginless)
                return true;

            return (clientPoint.X < _left ||
                    clientPoint.X > (clientSize.Width - _right) ||
                    clientPoint.Y < _top ||
                    clientPoint.Y > (clientSize.Height - _bottom));
        }

        public override string ToString() {
            return string.Format("Margins [{0},{1},{2},{3}]", _left, _top, _right, _bottom);
        }

        /// <summary>
        /// Gets the margins value as a padding instance.
        /// </summary>
        /// <returns></returns>
        public Padding AsPadding() {
            return new Padding(_left, _top, _right, _bottom);
        }

        /// <summary>
        /// Gets a static readonly 0-pixel margin.
        /// This margin returns true on the IsNull property.
        /// </summary>
        public static readonly Margins Zero = new Margins(0);

    }

}
