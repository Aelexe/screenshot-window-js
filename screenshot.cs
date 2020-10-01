#r "System.Drawing.dll"

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class Startup
{
	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	[StructLayout(LayoutKind.Sequential)]
	private struct Rect
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

	[DllImport("user32.dll")]
	private static extern IntPtr GetClientRect(IntPtr hWnd, ref Rect rect);

	[DllImport("user32.dll")]
	private static extern IntPtr ClientToScreen(IntPtr hWnd, ref Point point);

	/*
        Returns a screenshot as a byte array of the window matching the window name, or the foreground window if no match is found.
	*/
	public async Task<object> Invoke(String windowName)
	{
		// Get the window handle matching the provided window name.
		IntPtr windowHandle = IntPtr.Zero;
		foreach (Process pList in Process.GetProcesses())
		{
			if (pList.MainWindowTitle.Equals(windowName))
			{
				windowHandle = pList.MainWindowHandle;
				break;
			}
		}

		// If no matching window is found get the foreground window handle instead.
		if (windowHandle == (IntPtr)0)
		{
			windowHandle = GetForegroundWindow();
		}

		return ImageToByte(CaptureWindow(windowHandle));
	}

	/*
        Returns an image as a byte array.
    */
	public static byte[] ImageToByte(Image image)
	{
		ImageConverter converter = new ImageConverter();
		return (byte[])converter.ConvertTo(image, typeof(byte[]));
	}

	/*
        Returns a bitmap image of a window matching the provided window handle.
    */
	public static Bitmap CaptureWindow(IntPtr handle)
	{
		// Determine the windows point and bounds on the screen.
		var rect = new Rect();
		GetClientRect(handle, ref rect);

		var point = new Point(0, 0);
		ClientToScreen(handle, ref point);

		var bounds = new Rectangle(point.X, point.Y, rect.Right, rect.Bottom);
		var result = new Bitmap(bounds.Width, bounds.Height);

		// Capture the window.
		using (var graphics = Graphics.FromImage(result))
		{
			graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
		}

		return result;
	}
}
