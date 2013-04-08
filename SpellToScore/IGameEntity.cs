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
    public interface IGameEntity
    {
        // Called once per frame
        void Update(Canvas canvas);

        // Canvas passed so width and height can be calculated
        void Move(Direction direction, Canvas canvas);
    }
}