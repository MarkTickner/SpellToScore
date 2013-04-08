using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SpellToScore
{
    public class Scoring
    {
        private int wordScore;
        private int numberScore;

        public int WordScore(string word)
        {
            wordScore = 0;

            foreach (char c in word)
            {
                string character = c.ToString();

                if (character.Equals("A") || character.Equals("E") || character.Equals("I") || character.Equals("L") || character.Equals("N") || character.Equals("O") || character.Equals("R") || character.Equals("S") || character.Equals("T") || character.Equals("U"))
                {
                    wordScore = wordScore + 1;
                }
                else if (character.Equals("D") || character.Equals("G"))
                {
                    wordScore = wordScore + 2;
                }
                else if (character.Equals("B") || character.Equals("C") || character.Equals("M") || character.Equals("P"))
                {
                    wordScore = wordScore + 3;
                }
                else if (character.Equals("F") || character.Equals("H") || character.Equals("V") || character.Equals("W") || character.Equals("Y"))
                {
                    wordScore = wordScore + 4;
                }
                else if (character.Equals("K"))
                {
                    wordScore = wordScore + 5;
                }
                else if (character.Equals("J") || character.Equals("X"))
                {
                    wordScore = wordScore + 8;
                }
                else if (character.Equals("Q") || character.Equals("Z"))
                {
                    wordScore = wordScore + 10;
                }
            }

            return wordScore;
        }

        public int NumberScore(int number)
        {
            numberScore = 0;

            // Check if the number is even
            if (number % 2 == 0)
            {
                // Number is even
                numberScore = 2;
            }
            else
            {
                // Number is odd
                numberScore = -1;
            }

            return numberScore;
        }
    }
}