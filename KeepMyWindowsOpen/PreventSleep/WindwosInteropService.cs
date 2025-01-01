using System.Runtime.InteropServices;

namespace PreventSleep
{
    public class WindwosInteropService
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);
        [DllImport("kernel32.dll")]
        private static extern uint SetThreadExecutionState(uint esFlags);

        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_DISPLAY_REQUIRED = 0x00000002;


        public static bool IsMouseMoving = false;

        public void MoveMouse()
        {
            Task.Run(() =>
            {
               
                IsMouseMoving = true;
                while (IsMouseMoving)
                {
                    // Simulate a slight mouse movement
                    mouse_event(MOUSEEVENTF_MOVE, 0, 50, 0, UIntPtr.Zero);
                    Thread.Sleep(5000); // Wait 5 seconds
                    mouse_event(MOUSEEVENTF_MOVE, 0, -50, 0, UIntPtr.Zero);
                    Thread.Sleep(5000); // Wait 5 seconds
                }
            });

        }


        public void PreventSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED);
        }




        public void StopMouse()
        {
            IsMouseMoving = false;
        }

        public bool MouseIsMoving()
        {
            return IsMouseMoving;
        }
    }
}
