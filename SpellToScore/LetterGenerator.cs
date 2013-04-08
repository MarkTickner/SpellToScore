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
    public class LetterGenerator
    {
        // Letter frequency based on: http://en.wikipedia.org/wiki/Letter_frequency
        string[] letters = new string[]{
            "A",
            "A",
            "A",
            "A",
            "A",
            "A",
            "A",
            "A",
            "B",
            "C",
            "C",
            "C",
            "D",
            "D",
            "D",
            "D",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "E",
            "F",
            "F",
            "G",
            "G",
            "H",
            "H",
            "H",
            "H",
            "H",
            "H",
            "I",
            "I",
            "I",
            "I",
            "I",
            "I",
            "I",
            "J",
            "K",
            "L",
            "L",
            "L",
            "L",
            "M",
            "M",
            "N",
            "N",
            "N",
            "N",
            "N",
            "N",
            "N",
            "O",
            "O",
            "O",
            "O",
            "O",
            "O",
            "O",
            "O",
            "P",
            "P",
            "Q",
            "R",
            "R",
            "R",
            "R",
            "R",
            "R",
            "S",
            "S",
            "S",
            "S",
            "S",
            "S",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "T",
            "U",
            "U",
            "U",
            "V",
            "W",
            "W",
            "X",
            "Y",
            "Y",
            "Z"
        };

        public string GetRandomLetter()
        {
            Random rand = new Random();
            int aNumber = rand.Next(0, letters.Length);
            return letters[aNumber];
        }
    }
}