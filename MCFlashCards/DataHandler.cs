using Microsoft.VisualBasic;
//using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
namespace MCFlashCards
{

	public class DataHandler
	{
		private string FileName;
		public Item[] Cards;
		public int NCards;

		public string Description;

		public DataHandler()
		{
		}

		//Doesn't properly deal with escaping "
		public string[] SplitCSV(string inputText)
		{
			return Regex.Split(inputText, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
		}


		public void ReadCSV(string FileName)
		{
		}

		public void LoadCSVFile(string FName)
		{
			Microsoft.VisualBasic.FileIO.TextFieldParser afile = new Microsoft.VisualBasic.FileIO.TextFieldParser(FName);
			string[] CurrentRecord = null;
			// this array will hold each line of data
			afile.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
			afile.Delimiters = new string[] { "," };
			afile.HasFieldsEnclosedInQuotes = true;

			string[] StringData = new string[4];

			Description = afile.ReadFields()[0];
			CurrentRecord = null;
			//avoid the annoying warning about the uninitialized variable.

			Cards = new Item[5001];
			int CardNum = 0;
			string question = null;
			string answer = null;
			string[] WrongAnswers = new string[4];

			CardNum = 0;

			//"Tabbed" format one item per line
			// parse the actual file
			while (!afile.EndOfData) {
				try {
					CurrentRecord = afile.ReadFields();
				} catch (Microsoft.VisualBasic.FileIO.MalformedLineException ex) {
					System.Diagnostics.Debugger.Break();
				}

				//Example test line: """, comma test",", on the next part with """
				//Look for first identifier quotation mark or comma.
				//If Comma first, then take everything up to the comma and consider it as a cell.
				//If Quotation mark first, then read as a quotation mark string.
				//If quotation mark found, check next character, if another quotation mark, consider data as a single quotation mark (Escaped quotation mark)
				//Keep going ignoring commas until an unescaped quotation mark is found.  That is the cell.

				StringData = CurrentRecord;

				if ((StringData.GetUpperBound(0) >= 0)) {
					question = StringData[0];
				} else {
					question = "";
					Interaction.MsgBox("Error reading file.. continuing anyway..");
					break; // TODO: might not be correct. Was : Exit Do
				}

				if ((StringData.GetUpperBound(0) >= 1)) {
					answer = StringData[1];
				} else {
					answer = "";
					Interaction.MsgBox("Error reading file.. continuing anyway..");
					break; // TODO: might not be correct. Was : Exit Do
				}
				//clear WrongAnswers:
				for (int i = 0; i <= 2; i++) {
					WrongAnswers[i] = "";
				}

				//read in the incorrect answers in the next 3 columns if they exists
				if ((StringData.GetUpperBound(0) >= 2)) {
					WrongAnswers[0] = StringData[2];
				}
				if ((StringData.GetUpperBound(0) >= 3)) {
					WrongAnswers[1] = StringData[3];
				}
				if ((StringData.GetUpperBound(0) >= 4)) {
					WrongAnswers[2] = StringData[4];
				}

				Cards[CardNum] = new Item();
				//Cards(CardNum).Question = StringData(0)
				//Cards(CardNum).Characters = StringData(1)  '->2
				//Cards(CardNum).Answer = StringData(3)  '->1
				SetCard(CardNum, question, answer, WrongAnswers[0], WrongAnswers[1], WrongAnswers[2]);
				CardNum = CardNum + 1;
			}

			//Use Preserve keyword to retain the data within the resized array:
			NCards = CardNum;
			Array.Resize(ref Cards, CardNum);
		}

		public void LoadTxtFile(string FName)
		{
			System.IO.StreamReader IStream = null;
			string[] StringData = new string[4];
			string DataLine = null;

			//diag code:
			//NCards = 6
			//ReDim Cards(NCards - 1)
			//For i = 0 To NCards - 1
			//    Cards(i) = New Item
			//Next

			//SetCard(0, "1", "One")
			//SetCard(1, "2", "Two")
			//SetCard(2, "3", "Three")
			//SetCard(3, "4", "Four")
			//SetCard(4, "5", "Five")
			//SetCard(5, "6", "Six")


			//NCards = 0
			//IStream = System.IO.File.OpenText(FName)
			//Description = IStream.ReadLine()
			//DataType = IStream.ReadLine()              'skip this line

			//If InStr(DataType, "tab") Then
			//    '"Tabbed" format one item per line
			//    While Not IStream.EndOfStream
			//        DataLine = IStream.ReadLine()
			//        If DataLine <> "" Then
			//            NCards = NCards + 1                 'determine number of items
			//        End If
			//    End While
			//    IStream.Close()

			//Else
			//    '"normal" format line by line
			//    While Not IStream.EndOfStream
			//        DataLine = ""
			//        For i = 0 To 3
			//            DataLine = IStream.ReadLine()
			//        Next
			//        If DataLine <> "" Then
			//            NCards = NCards + 1                 'determine number of items
			//        End If
			//    End While
			//    IStream.Close()
			//End If

			//Start over:
			IStream = System.IO.File.OpenText(FName);
			Description = IStream.ReadLine();

			Cards = new Item[5001];
			int CardNum = 0;
			string question = null;
			string answer = null;

			CardNum = 0;
			//"Tabbed" format one item per line
			while (!IStream.EndOfStream) {
				DataLine = IStream.ReadLine();

				if (!string.IsNullOrEmpty(DataLine)) {
					StringData = Strings.Split(DataLine, Constants.vbTab);

					if ((StringData.GetUpperBound(0) >= 0)) {
						question = StringData[0];
					} else {
						question = "";
						Interaction.MsgBox("Error reading file.. continuing anyway..");
						break; // TODO: might not be correct. Was : Exit While
					}

					if ((StringData.GetUpperBound(0) >= 1)) {
						answer = StringData[1];
					} else {
						answer = "";
						Interaction.MsgBox("Error reading file.. continuing anyway..");
						break; // TODO: might not be correct. Was : Exit While
					}

					Cards[CardNum] = new Item();
					//Cards(CardNum).Question = StringData(0)
					//Cards(CardNum).Characters = StringData(1)  '->2
					//Cards(CardNum).Answer = StringData(3)  '->1
					SetCard(CardNum, question, answer, "", "", "");
					CardNum = CardNum + 1;
				}
			}

			//Use Preserve keyword to retain the data within the resized array:
			NCards = CardNum;
			Array.Resize(ref Cards, CardNum);
		}

		public void LoadFile(string FName)
		{
			if ((FName.Contains(".txt"))) {
				LoadTxtFile(FName);
			} else if ((FName.Contains(".csv"))) {
				LoadCSVFile(FName);
			} else {
				MessageBox.Show("Unrecognized file type.");
			}
		}

		private void SetCard(int Index, string Ques, string Ans, string Wrong0, string Wrong1, string Wrong2)
		{
			Cards[Index].Index = Index;
			Cards[Index].Question = Ques;
			Cards[Index].Answer = Ans;
			//Cards(Index).Characters = Chr
			Cards[Index].WrongAnswers[0] = Wrong0;
			Cards[Index].WrongAnswers[1] = Wrong1;
			Cards[Index].WrongAnswers[2] = Wrong2;

			//count how many wrong answers we have:
			Cards[Index].NWrongAnswers = 0;
			for (int i = 0; i <= 2; i++) {
				if (!string.IsNullOrEmpty(Cards[Index].WrongAnswers[i])) {
					Cards[Index].NWrongAnswers = Cards[Index].NWrongAnswers + 1;
				} else {
					//exit at the first empty string
					break; // TODO: might not be correct. Was : Exit For
				}
			}
		}

	}
}
