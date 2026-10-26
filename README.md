MCFlashCards
===========

Multiple Choice Flashcards.  This program is useful for memorizing lots of things.  You can set the program
to ask questions and provide multiple choice answers or provide answers and choose the correct question.

The questions are ordered such that after learning the first 4 items, only 2 new items are added at a time.
This way, the most recently learned questions are repeated very shortly after learning them to reinforce
the learned item.  Try it out and see how it works.

If only question and answer are given in 2 columns in the csv, then the other choices provided will be 
taken from the answers of surrounding questions.
Alternately, incorrect answers can be specified within the csv file in 3 columns to the right of the question and 
answer columns.  

The data is easily entered using a CSV format which can be created using Excel, Google Sheets, 
or an AI like ChatGPT.  See the example items in the Release folder.

For any questions answered incorrectly, the questions are added to a file with a suffix "_incorrect" added
which can be used to further study the questions that were incorrectly answered the first attempt.
Note items will not automatically be removed from the _incorrect file.

ChatGPT or other AI can generate the data in a useable format easily from a study guide for example.  
Usually this set of two requests gets the best results.  If the input source is a file, upload the file along with the
first request.  If the input source is text, just paste the data below the first request.
1. Translate this into a question and answer quiz in csv format where the first column is the question and the second is the answer
2. Now take the csv data and add 3 more columns to the right with incorrect answers like in a multiple choice test.

Settings
--------

The settings are kept in a file named MCFlashCards.ini which sits in the same folder as the .exe, so they stay
with the program and are read back the next time it is run.

One setting is offered at the moment, the "ntfy channel", shown on the main form.  Put the name of a channel
(see https://ntfy.sh) in the text box and, when a set of cards has been completed, MCFlashCards sends a message
to that channel through ntfy, so the result can be read on a phone or another computer.  Leave the text box
blank, which is the default, and nothing at all is sent.  A whole address may be used instead of a channel name
to send to a private ntfy server.  A notification which cannot be sent is only noted in the log; it never
upsets a study session.

Testing
--------

The settings, and the notification sent when a set is completed, are checked by a unit test which needs no
window and no study session: from the command line,

    MCFlashCards.exe --test

prints one line per check and ends with the number of checks which failed as its exit status, so zero means
all of them passed.  The tests keep their own settings file, MCFlashCards-unit-test.ini, next to the .exe, and
never read or write MCFlashCards.ini, so they cannot change the channel you are subscribed to; the
notifications they send go to topics of their own which nothing else reads.  The few checks which really
reach ntfy need a connection, and say so when there isn't one.

![ScreenShot](https://github.com/jdunne525/MCFlashCards/blob/master/screenshot.PNG?raw=true)