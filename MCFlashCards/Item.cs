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
namespace MCFlashCards
{
	public class Item
	{
		public string Question;
		public string Answer;
		public string[] WrongAnswers = new string[4];
		public int Index;

		public int NWrongAnswers;
		public Item()
		{
			Question = "";
			Answer = "";
			//clear WrongAnswers:
			for (int i = 0; i <= 3; i++) {
				WrongAnswers[i] = "";
			}
		}
	}
}
