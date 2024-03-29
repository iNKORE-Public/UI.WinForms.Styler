﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace iNKORE.UI.WinForms.Styler.Native {
	internal static class Windows {

		/// <summary>Returns the active windows on the current thread.</summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetWindowRect(IntPtr hwnd, out RECT rect);

	}
}
