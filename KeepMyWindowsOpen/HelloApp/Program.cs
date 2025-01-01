using PreventSleep;

namespace HelloApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the NotifyIcon
            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = new Icon(SystemIcons.Application, 40, 40), // Use default system icon
                Visible = true,
                Text = "My Tray App"
            };

            var mouseMessage = "M M ";

            var windowsService = new WindwosInteropService();
            // Create a context menu for the tray icon
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add(mouseMessage, null, (s, e) => {
                if (windowsService.MouseIsMoving())
                {
                    windowsService.StopMouse();
                    return;
                }
                windowsService.MoveMouse();
            });
            contextMenu.Items.Add("P S", null, (s, e) => windowsService.PreventSleep());
            contextMenu.Items.Add("Exit", null, (s, e) =>
            {
                notifyIcon.Visible = false; // Hide the tray icon
                Application.Exit(); // Exit the application
            });

            notifyIcon.ContextMenuStrip = contextMenu;

            // Run the application without a visible form
            Application.Run(); 
        }
    }
}