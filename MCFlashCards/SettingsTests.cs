namespace MCFlashCards
{
    /// <summary>
    /// Unit tests for the settings, and in particular for the notification which is
    /// pushed to the ntfy channel when a set of cards has been completed.
    ///
    /// They are run from the command line, with no window opened:
    ///
    ///     MCFlashCards.exe --test
    ///
    /// Every check prints one line, and the program ends with the number of checks
    /// which failed, so zero means all of them passed.
    ///
    /// The tests work with their own settings file, MCFlashCards-unit-test.ini, which
    /// sits next to the executable.  They never read or write MCFlashCards.ini, so
    /// running the tests cannot change the channel you are subscribed to, and the
    /// notifications they send go to topics of their own which nothing else reads.
    /// The checks which send something need a connection; without one they say so.
    /// </summary>
    public class SettingsTests
    {
        private int Checks;
        private int Failed;

        public SettingsTests()
        {
            Checks = 0;
            Failed = 0;
        }

        /// <summary>
        /// The settings file a test reads or writes, which is not the one the program
        /// itself uses.
        /// </summary>
        private static String TestSettingsFile()
        {
            return Application.StartupPath + "\\MCFlashCards-unit-test.ini";
        }

        /// <summary>
        /// Settings which read and write that test file, so the tests are alone.
        /// </summary>
        private static Settings TestSettings()
        {
            return new Settings(TestSettingsFile());
        }

        private void Check(bool Passed, String What)
        {
            Checks = Checks + 1;
            if (Passed)
            {
                Console.WriteLine("  ok      " + What);
            }
            else
            {
                Failed = Failed + 1;
                Console.WriteLine("  FAILED  " + What);
            }
        }

        private void Note(String Text)
        {
            Console.WriteLine("          " + Text);
        }

        private void Test_TheChannelBeingBlankMeansNothingAtAllIsSent()
        {
            Note("no channel set, so nothing must be sent");
            Settings S = TestSettings();
            S.SetNtfyChannel("");
            Check(S.NtfyChannel == "", "the empty channel stays empty after it is set");
            Check(S.NtfyUrl(S.NtfyChannel) == "", "an empty channel gives no address to post to");
            bool Sent = S.SendNotification("MCFlashCards unit test", "nothing should be sent at all");
            Check(Sent == false, "no notification is reported as sent");
            Check(S.SendCount == 0, "not even an attempt is made, so nothing goes out");
            Check(S.LastNotificationError == "", "nothing is blamed for a send which was never tried");
        }

        private void Test_AChannelNameIsPostedToThePublicServer()
        {
            Note("only a topic name is given, so the address is built for the public server");
            Settings S = TestSettings();
            S.SetNtfyChannel("  study-cards-7  ");
            Check(S.NtfyChannel == "study-cards-7", "spaces around a channel typed on the form are trimmed");
            Check(S.NtfyUrl("study-cards-7") == "https://ntfy.sh/study-cards-7", "a topic name becomes an address on ntfy.sh");
            Check(S.NtfyUrl(S.NtfyChannel) == "https://ntfy.sh/study-cards-7", "the channel on the settings gives that address");
        }

        private void Test_AWholeAddressIsUsedAsItStands()
        {
            Note("a whole address may be typed instead, for a private ntfy server");
            Settings S = TestSettings();
            Check(S.NtfyUrl("https://ntfy.private.example/quiet-cards") == "https://ntfy.private.example/quiet-cards", "a whole https address is used as it stands");
            Check(S.NtfyUrl("http://ntfy.private.example/quiet-cards") == "http://ntfy.private.example/quiet-cards", "a whole http address is used as it stands");
            Check(S.NtfyUrl("HTTPS://ntfy.private.example/quiet-cards") == "HTTPS://ntfy.private.example/quiet-cards", "an address written in upper case is still seen as an address");
        }

        private void Test_AChannelIsKeptWithTheProgramAndReadBack()
        {
            Note("the settings stay with the executable and are read back the next time");
            Settings Written = TestSettings();
            Written.SetNtfyChannel("class-history-3");
            Settings ReadBack = TestSettings();
            Check(ReadBack.NtfyChannel == "class-history-3", "a channel set once is read back from the file next to the .exe");
            Check(ReadBack.SendCount == 0 && ReadBack.LastNotificationError == "", "reading the settings alone sends nothing");
            Settings None = TestSettings();
            None.SetNtfyChannel("");
            Settings ReadBackBlank = TestSettings();
            Check(ReadBackBlank.NtfyChannel == "", "an empty channel is kept as empty, which is the default");
        }

        private void Test_AFinishedSetSendsANotification()
        {
            // This is the reported behaviour: a round was finished and the Done box
            // showed, so the set is complete and a message must reach ntfy.  The call
            // below is the one frmMain makes at that moment, with the same kind of
            // words, including some outside plain ascii.
            Note("a completed set must reach ntfy, which needs a connection");
            Settings S = TestSettings();
            S.SetNtfyChannel("mcflashcards-unit-test-cards");
            int Before = S.SendCount;
            bool Sent = S.SendNotification(
                "MCFlashCards set completed",
                "Set completed: World History \u00E9 final exam \u2014 caf\u00E9, first try correct 95%, best streak 16, completed 20, incorrect 4");
            Check(Sent, "the notification for a completed set is sent and ntfy accepts it");
            Check(S.SendCount == Before + 1, "one send is counted for the completed set");
            Check(S.LastNotificationError == "", "the send is not left to fail quietly: nothing is noted against it");
        }

        private void Test_AHeadingOutsidePlainWordsIsStillSent()
        {
            Note("ntfy takes headings in plain words only, so they are trimmed to that");
            Settings S = TestSettings();
            S.SetNtfyChannel("mcflashcards-unit-test-heading");
            bool WithForeignHeading = S.SendNotification(
                "\u4ED8\u6BC2 MCFlashCards \u2713",
                "a heading outside plain words must still send");
            Check(WithForeignHeading, "a heading with signs outside plain ascii is sent anyway");
            bool OnlyForeignHeading = S.SendNotification(
                "\u4ED8\u6BC2 \u2713",
                "a heading with nothing plain left falls back to the program name");
            Check(OnlyForeignHeading, "even a heading with nothing plain in it is sent");
            Check(S.SendCount == 2, "both of those sends were attempted");
        }

        private void Test_AMessageLongerThanNtfyTakesIsShortened()
        {
            Note("a long description, some of it outside plain ascii, must be shortened, not refused");
            String Long = "";
            for (int i = 0; i <= 200; i = i + 1)
            {
                Long = Long + "0123456789abcdefghijklmnopqrstuvwxyz \u00E9\u4ED8";
            }
            Settings S = TestSettings();
            S.SetNtfyChannel("mcflashcards-unit-test-long");
            bool Sent = S.SendNotification("MCFlashCards unit test", Long);
            Check(Sent, "a message longer than ntfy takes is shortened and still sent");
        }

        private void Test_ASendWhichCannotWorkSaysSo()
        {
            Note("a channel which cannot be reached must be explained, not silent");
            Settings S = TestSettings();
            S.SetNtfyChannel("http://127.0.0.1:1/mcflashcards-nothing-listens-here");
            bool Sent = S.SendNotification("MCFlashCards unit test", "nothing listens at that address");
            Check(Sent == false, "a send which cannot be made is not called a success");
            Check(S.SendCount == 1, "the attempt is still counted, so a test can tell it was tried");
            Check(S.LastNotificationError != "", "what went wrong is kept, ready for the log and for a test");
        }

        /// <summary>
        /// Runs all the checks and says how many failed, which the command line hands
        /// back as the program's exit status.  The checks which need a connection come
        /// last, so a poor connection cannot hide a fault in the plain logic above.
        /// </summary>
        public int RunAllTests()
        {
            Console.WriteLine("MCFlashCards settings tests");
            Console.WriteLine("---------------------------");
            Console.WriteLine("the settings file used by the tests is " + TestSettingsFile());
            Console.WriteLine();
            Test_TheChannelBeingBlankMeansNothingAtAllIsSent();
            Test_AChannelNameIsPostedToThePublicServer();
            Test_AWholeAddressIsUsedAsItStands();
            Test_AChannelIsKeptWithTheProgramAndReadBack();
            Test_AFinishedSetSendsANotification();
            Test_AHeadingOutsidePlainWordsIsStillSent();
            Test_AMessageLongerThanNtfyTakesIsShortened();
            Test_ASendWhichCannotWorkSaysSo();
            Console.WriteLine();
            if (Failed == 0)
            {
                Console.WriteLine("all " + Checks + " checks passed.");
            }
            else
            {
                Console.WriteLine(Failed + " of " + Checks + " checks failed.");
            }
            return Failed;
        }
    }
}
