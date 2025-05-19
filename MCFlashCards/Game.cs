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
using System.Numerics;
namespace MCFlashCards
{
	public class Game
	{
		public bool GameRunning;
		public Item AnswerToDisplay;

		public Item[] CardsToDisplay = new Item[4];
		public Item[] CardsDisplayed = new Item[4];
		public int CurrentItemSet;
		public int NumberOfRndItemSets;
		public bool AllRandom;

		public int NItemSetsInData;
		private Item[] CardsInPlay = new Item[4];
		private int CurrentItem;
		private DataHandler Data;

		private int LastCardShown;
		public Game(DataHandler DHandler)
		{
			Data = DHandler;
			GameRunning = false;
			NumberOfRndItemSets = 2;
			for (int i = 0; i <= 3; i++) {
				CardsToDisplay[i] = new Item();
			}
		}

		//Returns True if Game started.
		public bool StartGame()
		{
			GameRunning = false;
			if (Data.NCards < 4) {
				return false;
				//Not enough cards to play.
			}

			NItemSetsInData = (Data.NCards - 4) / 2 + 1;
			//This works because NItemSetsInData MUST be > 4

			LastCardShown = 3;
			//we're about to show cards 0 through 4:
			for (int i = 0; i <= 3; i++) {
				CardsInPlay[i] = Data.Cards[i];
				//load first 4 cards

				CardsToDisplay[i] = CardsInPlay[i];
				//Load up display cards
			}

			RandomizeArray(CardsToDisplay);
			//Randomize the cards to display
			RandomizeArray(CardsInPlay);
			//Randomize the order in which to Request them
			CurrentItemSet = 0;
			CurrentItem = 0;
			AnswerToDisplay = CardsInPlay[CurrentItem];
			GameRunning = true;
			return true;
		}

		public bool NextQuestion(bool AutoIncrement)
		{
			if (CurrentItem < 3) {
				RandomizeArray(CardsToDisplay);
				//Randomize the cards to display
				CurrentItem = CurrentItem + 1;
				//go to the next question
				AnswerToDisplay = CardsInPlay[CurrentItem];

			} else {
				//If LastCardShown + 2 >= Data.NCards Then
				//    'We're out of new cards.
				//    Return False
				//End If

				if (AutoIncrement) {
					CurrentItemSet = CurrentItemSet + 1;
				}

				if (CurrentItemSet >= NItemSetsInData + NumberOfRndItemSets) {
					CurrentItemSet = NItemSetsInData + NumberOfRndItemSets - 1;
					GameRunning = false;
					return false;
					//Done.
				}

				int NumOfRndCards = 0;

				LastCardShown = 3 + CurrentItemSet * 2;
				if (LastCardShown >= Data.NCards) {
					LastCardShown = Data.NCards - 1;
				}

				if ((CurrentItemSet == 0)) {
					NumOfRndCards = 4;
				} else {
					NumOfRndCards = 2;
				}

				if (Data.NCards - LastCardShown - 1 < 2) {
					NumOfRndCards = 4 - (Data.NCards - LastCardShown - 1);
					if (NumOfRndCards > 4) {
						NumOfRndCards = 4;
					}
				}
				if (AllRandom) {
					NumOfRndCards = 4;
					LastCardShown = Data.NCards;
				}

				Item[] RndCards = new Item[NumOfRndCards];

				//'Grab 2 random cards from the previous set:
				//Dim rnd As New Random
				//'Get 2 random cards which are not the same from the previous data sets:
				//RndCard1 = RandomNumber(0, LastCardShown)            'pick one of the previous cards
				//Do
				//    RndCard2 = rnd.Next(0, LastCardShown)
				//    If RndCard1 <> RndCard2 Then
				//        Exit Do                                 'loop until we have 2 distinct cards
				//    End If
				//Loop

				GetRandomCards(NumOfRndCards, LastCardShown, ref RndCards, ref Data.Cards);
				//Get 2 random cards

				//Load 4 new cards:
				for (int i = 1; i <= NumOfRndCards; i++) {
					CardsInPlay[i - 1] = RndCards[i - 1];
					//put the random cards into the set
				}

				for (int i = NumOfRndCards; i <= 3; i++) {
					CardsInPlay[i] = Data.Cards[LastCardShown + 1];
					//put the Next cards into the set
					LastCardShown = LastCardShown + 1;
				}

				for (int i = 0; i <= 3; i++) {
					CardsToDisplay[i] = CardsInPlay[i];
					//Load up display cards
				}

				RandomizeArray(CardsToDisplay);
				//Randomize the cards to display
				RandomizeArray(CardsInPlay);
				//Randomize the order in which to Request them

				CurrentItem = 0;
				AnswerToDisplay = CardsToDisplay[CurrentItem];
			}

			string tempval = null;
			for (int i = 0; i <= 3; i++) {
				tempval = CardsInPlay[i].Answer;
			}

			return true;
		}

		private void GetRandomCards(int Number, int LastItem, ref Item[] RndCards, ref Item[] DataSet)
		{
            //initialize random number generator
            Random r = new Random(System.DateTime.Now.Millisecond);

            

            //Dim rnd As New Random
            int NewCard = 0;
			int[] Cards = new int[Number];
			int Attempts = 0;

			if (Number == 0)
				return;

			for (int i = 0; i < Number; i++) {
				Cards[i] = -1;
				//initialize with nonzero values so we can put 0 wherever we want
			}

			Cards[0] = r.Next(0, LastItem + 1);			//RandomNumber(0, LastItem + 1);

            //pick one of the previous cards
            for (int i = 1; i < Number; i++) {

				//Note this was rewritten.

				//Check for a duplicate:
				Attempts = 0;

				NewCard = r.Next(0, LastItem + 1);
				while (IsDuplicate(NewCard, Cards))
				{
                    Attempts++;
					NewCard = r.Next(0, LastItem + 1);		//RandomNumber(0, LastItem + 1);
                    if (Attempts > 50)
                    {
                        //possible infinite loop.  Just give up.
                        Debug.WriteLine("Infinite loop in GetRandomCards");
                        break;
                    }
                }

     //           for (int j = 0; j < i; j++) {
     //               if (NewCard == Cards[j]) {
     //                   NewCard = RandomNumber(0, LastItem + 1);
     //                   j = 0;
					//	Attempts++;
     //               }
					//if (Attempts > 50)
					//{
     //                   //possible infinite loop.  Just give up.
					//	Debug.WriteLine("Infinite loop in GetRandomCards");
     //                   break;
					//}
     //           }
                Cards[i] = NewCard;
                //The New card is good, add it to our Cards list
			}

			for (int i = 0; i <= Number - 1; i++) {
				RndCards[i] = DataSet[Cards[i]];
				//copy the random cards from the full dataset to the Random card set.
			}
		}

		private bool IsDuplicate(int NewCard, int[] Cards)
		{
            for (int i = 0; i < Cards.Length; i++) {
                if (NewCard == Cards[i]) {
                    return true;
                }
            }
            return false;
		}

		public bool SetItemSet(int NewItemSet)
		{
			int NItemSetsInData = 0;
			NItemSetsInData = (Data.NCards - 4) / 2 + 1;
			//This works because NItemSetsInData MUST be > 4

			if ((NewItemSet < 0))
				NewItemSet = 0;


			CurrentItemSet = NewItemSet;

			if (CurrentItemSet >= NItemSetsInData + NumberOfRndItemSets) {
				CurrentItemSet = NItemSetsInData + NumberOfRndItemSets - 1;
				//-2 so that when we go to the NextQuestion
			}
			CurrentItem = 3;
			return NextQuestion(false);
		}

		public bool CheckAnswer(string AnswerClicked)
		{
			//If CardsToDisplay(Index - 1).Index = AnswerToDisplay.Index Then
			//Check that the answer is correct, not just that it's the exact matching card we were looking for:
			if (AnswerClicked == AnswerToDisplay.Answer) {
				return true;
			} else {
				return false;
			}
		}

		private void RandomizeArray(Item[] items)
		{
			for (int count = 1; count <= 10; count++) {
				int max_index = items.Length - 1;
				//Dim rnd As New Random
				for (int i = 0; i <= max_index - 1; i++) {
					// Pick an item for position i.
					int j = RandomNumber(i, max_index + 1);

					// Swap them.
					Item temp = items[i];
					items[i] = items[j];
					items[j] = temp;
				}
			}
		}
		private void RandomizeArray(int[] items)
		{
			for (int count = 1; count <= 10; count++) {
				int max_index = items.Length - 1;
				//Dim rnd As New Random
				for (int i = 0; i <= max_index - 1; i++) {
					// Pick an item for position i.
					int j = RandomNumber(i, max_index + 1);

					// Swap them.
					int temp = items[i];
					items[i] = items[j];
					items[j] = temp;
				}
			}
		}

		//MinNumber is inclusive and MaxNumber is exclusive
		public int RandomNumber(int MinNumber, int MaxNumber)
		{

			//initialize random number generator
			Random r = new Random(System.DateTime.Now.Millisecond);

			//if passed incorrect arguments, swap them
			//can also throw exception or return 0

			if (MinNumber > MaxNumber) {
				int t = MinNumber;
				MinNumber = MaxNumber;
				MaxNumber = t;
			}

			return r.Next(MinNumber, MaxNumber);
			//MaxNumber is Exclusive in this call!!

		}

		public void SaveResults(string filename, string header, string Line)
		{
			//Save results to csv file
			//Append to existing file
			//check if the file exists and if not, create it and append the header

			if (!System.IO.File.Exists(filename)) {
				System.IO.FileStream fso1 = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
				System.IO.StreamWriter sw1 = new System.IO.StreamWriter(fso1);
				sw1.WriteLine(header);
				sw1.Close();
				fso1.Close();
			}

			System.IO.FileStream fso = new System.IO.FileStream(filename, System.IO.FileMode.Append, System.IO.FileAccess.Write);
			System.IO.StreamWriter sw = new System.IO.StreamWriter(fso);

			sw.WriteLine(Line);
			sw.Close();
			fso.Close();


		}

	}
}
