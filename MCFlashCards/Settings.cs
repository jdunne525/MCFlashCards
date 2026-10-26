using System.Windows.Forms;
namespace MCFlashCards
{
    /// <summary>
    /// Keeps the program's settings.  The settings are stored in a small text file
    /// kept in the same folder as the executable, so they stay with the program and
    /// are read back the next time it runs.
    ///
    /// They can be tested from the command line without opening a window:
    ///
    ///     MCFlashCards.exe --test
    ///
    /// See SettingsTests.cs, which checks the notification sent when a set has been
    /// completed, the behaviour which matters here.
    /// </summary>
    public class Settings
    {
        // The ntfy topic (channel) a notification is pushed to when a set has been
        // completed.  An empty channel means nothing is sent.
        public String NtfyChannel;

        private String SettingsFileName;

        // What the last notification did, so a test, or the log, can tell whether a
        // send was really attempted and what went wrong when it did not work.
        public int SendCount;
        public String LastNotificationError;

        public Settings()
        {
            // The settings file lives next to the .exe.
            SettingsFileName = Application.StartupPath + "\\MCFlashCards.ini";
            Load();
        }

        public Settings(String SettingsFile)
        {
            SettingsFileName = SettingsFile;
            Load();
        }

        /// <summary>
        /// Reads the settings from the file.  A missing file, or a line which is not
        /// recognised, leaves that setting at its default.
        /// </summary>
        public void Load()
        {
            NtfyChannel = "";
            SendCount = 0;
            LastNotificationError = "";
            if (!System.IO.File.Exists(SettingsFileName))
            {
                // No settings file yet: keep the defaults.
                return;
            }

            String[] Lines = System.IO.File.ReadAllLines(SettingsFileName);
            foreach (String Line in Lines)
            {
                String Text = Line.Trim();
                if (Text == "" || Text.StartsWith("#"))
                {
                    // Blank lines and comments are left alone.
                    continue;
                }
                if (Text.ToLower().StartsWith("ntfychannel="))
                {
                    NtfyChannel = Text.Substring("ntfychannel=".Length).Trim();
                }
            }
        }

        /// <summary>
        /// Writes the settings back to the file next to the executable.
        /// </summary>
        public void Save()
        {
            System.IO.FileStream fso = new System.IO.FileStream(SettingsFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fso);
            sw.WriteLine("# MCFlashCards settings.  Keep this file in the same folder as the executable.");
            sw.WriteLine("# NtfyChannel is the ntfy topic pushed to when a set has been completed.");
            sw.WriteLine("# Leave NtfyChannel empty to send no notification.");
            sw.WriteLine("NtfyChannel=" + NtfyChannel);
            sw.Close();
            fso.Close();
        }

        /// <summary>
        /// Sets the ntfy channel and saves it, so the setting is remembered next
        /// time the program is run.
        /// </summary>
        public void SetNtfyChannel(String Channel)
        {
            NtfyChannel = Channel.Trim();
            Save();
        }

        /// <summary>
        /// The address a notification is posted to.  A topic name is a channel on the
        /// public server; a whole address is used as it stands, which allows a
        /// private ntfy server.  No channel gives no address, and no notification.
        /// </summary>
        public String NtfyUrl(String Channel)
        {
            String Topic = Channel.Trim();
            if (Topic == "")
            {
                // No channel configured: there is nothing to post to.
                return "";
            }
            String TopicLower = Topic.ToLower();
            if (TopicLower.StartsWith("http://") || TopicLower.StartsWith("https://"))
            {
                // A whole address was given, which allows a private ntfy server.
                return Topic;
            }
            // Only the topic was given: it is a channel on the public server.
            return "https://ntfy.sh/" + Topic;
        }

        /// <summary>
        /// Pushes a notification through ntfy to the configured channel.
        /// Nothing at all is attempted when no channel is configured.  A failed or
        /// unreachable ntfy never disturbs the game: it is only noted in the log.
        /// Returns whether the notification was sent.
        /// </summary>
        public bool SendNotification(String title, String message)
        {
            String Url = NtfyUrl(NtfyChannel);
            if (Url == "")
            {
                // No channel configured: send nothing, and do not count an attempt.
                return false;
            }

            // Record the attempt, so a test can tell that a send really went out and
            // so a failure can be read from the log rather than guessed at.
            SendCount = SendCount + 1;
            LastNotificationError = "";

            // ntfy reads the topic from the address, the message from the body and
            // the heading from the Title header.  Headers must be plain ASCII.
            String Header = ToAscii(title);
            if (Header == "")
            {
                Header = "MCFlashCards";
            }

            // The message is posted as plain text, cut down to what ntfy accepts.
            String Body = NtfyBody(message);

            try
            {
                // One POST, with the message in the body and the ntfy settings as
                // headers.  Content-Type is left to the text body itself: the request
                // headers refuse a content header, and asking for one here is what
                // used to make every notification fail quietly, in the catch below.
                System.Net.Http.HttpRequestMessage Request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, Url);
                Request.Headers.Add("Title", Header);
                Request.Headers.Add("Tags", "bell,message");
                Request.Headers.Add("Priority", "high");
                Request.Headers.Add("User-Agent", "MCFlashCards");
                Request.Content = new System.Net.Http.StringContent(Body);
                System.Net.Http.HttpClient Client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage Response = Client.Send(Request);
                bool Ok = Response.IsSuccessStatusCode;
                if (!Ok)
                {
                    LastNotificationError = "ntfy answered HTTP " + Response.StatusCode + " " + Response.ReasonPhrase;
                    System.Diagnostics.Debug.WriteLine("ntfy notification failed: " + LastNotificationError);
                }
                return Ok;
            }
            catch (System.Exception E)
            {
                // No connection, or no ntfy: the game carries on regardless.
                LastNotificationError = "the send failed: " + E.Message;
                System.Diagnostics.Debug.WriteLine("ntfy notification failed: " + LastNotificationError);
                return false;
            }
        }

        /// <summary>
        /// Drops everything outside plain ASCII, which ntfy rejects in a header.
        /// </summary>
        private String ToAscii(String text)
        {
            String result = "";
            for (int i = 0; i < text.Length; i = i + 1)
            {
                if (text[i] >= 32 && text[i] < 127)
                {
                    // Keep just the plain ASCII characters.
                    result = result + text.Substring(i, 1);
                }
            }
            return result;
        }

        /// <summary>
        /// Shortens a message to the 4096 bytes ntfy accepts, which a set with a
        /// long description can pass.  Bytes, not characters, is what ntfy counts,
        /// so a letter outside plain ascii costs more than one of them. A pair of
        /// characters standing for one sign is counted a little high, which only
        /// ever cuts a message a touch earlier.
        /// </summary>
        private String NtfyBody(String text)
        {
            String result = "";
            int Bytes = 0;
            for (int i = 0; i < text.Length; i = i + 1)
            {
                int Char = text[i];
                int CharBytes = 1;
                if (Char > 127)
                {
                    CharBytes = 2;
                }
                if (Char > 2047)
                {
                    CharBytes = 3;
                }
                if (Bytes + CharBytes > 4096)
                {
                    // Everything past here is more than ntfy will take.
                    return result;
                }
                Bytes = Bytes + CharBytes;
                result = result + text.Substring(i, 1);
            }
            return result;
        }
    }
}
