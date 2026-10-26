namespace MCFlashCards
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(String[] Args)
        {
            //The unit tests can be run from the command line without opening a window:
            //MCFlashCards.exe --test  prints one line for every check, ends with the
            //number of checks which failed as its exit status, and opens no form.
            if (Args != null && Args.Length > 0 && Args[0] == "--test")
            {
                return new SettingsTests().RunAllTests();
            }

            //MCFlashCards.exe --notify  sends one notification to the channel kept in
            //the settings next to the .exe, which is what a finished set would send,
            //so the notification can be seen to work on a phone or another computer.
            if (Args != null && Args.Length > 0 && Args[0] == "--notify")
            {
                Settings MySettings = new Settings();
                bool Sent = MySettings.SendNotification(
                    "MCFlashCards set completed",
                    "Set completed: notification check, first try correct 100%, best streak 1, completed 1, incorrect 0");
                if (Sent)
                {
                    Console.WriteLine("the notification was sent to " + MySettings.NtfyChannel);
                    return 0;
                }
                if (MySettings.SendCount == 0)
                {
                    //No channel is set: this is the default, and nothing is wanted.
                    Console.WriteLine("no ntfy channel is set, so nothing was sent.");
                    return 0;
                }
                Console.WriteLine("the notification failed: " + MySettings.LastNotificationError);
                return 1;
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
            return 0;
        }
    }
}
