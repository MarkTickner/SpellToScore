using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpellToScore
{
    public partial class HowToPlay : UserControl
    {
        TextBlock instructionsTxt = new TextBlock();
        Image controlsImg = new Image();
        Button backToMenuBtn = new Button();

        public HowToPlay()
        {
            InitializeComponent();

            LayoutRoot.Width = 800;
            LayoutRoot.Height = 540;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("Images/how_to_play.png", UriKind.Relative));
            LayoutRoot.Background = ib;

            instructionsTxt.Text = "The aim of level one is to shoot letters to spell out colours. Words spelled are scored on the letters it's made up of, so more uncommon letters as well as longer words will score more highly. In level two you must hit as many even numbers as possible, but be careful not to hit odd numbers as you will lose points! There is a time limit, and you will be awarded a bonus for hitting parrots, and a penalty for hitting seagulls.";
            instructionsTxt.Width = 520;
            instructionsTxt.FontSize = 12;
            instructionsTxt.TextWrapping = TextWrapping.Wrap;
            Canvas.SetLeft(instructionsTxt, (LayoutRoot.Width / 2) - (instructionsTxt.Width / 2));
            Canvas.SetTop(instructionsTxt, 125);
            LayoutRoot.Children.Add(instructionsTxt);

            controlsImg.Source = new BitmapImage(new Uri("Images/how_to_play_controls.png", UriKind.Relative));
            controlsImg.Width = 475;
            controlsImg.Height = 220;
            Canvas.SetLeft(controlsImg, (LayoutRoot.Width / 2) - (controlsImg.Width / 2));
            Canvas.SetTop(controlsImg, 205);
            LayoutRoot.Children.Add(controlsImg);

            backToMenuBtn.Content = "Back to Menu";
            backToMenuBtn.Width = 150;
            backToMenuBtn.Height = 35;
            backToMenuBtn.FontSize = 16;
            backToMenuBtn.Click += new RoutedEventHandler(backToMenuBtn_Click);
            Canvas.SetLeft(backToMenuBtn, (LayoutRoot.Width / 2) - (backToMenuBtn.Width / 2));
            Canvas.SetTop(backToMenuBtn, 480);
            LayoutRoot.Children.Add(backToMenuBtn);
        }

        private void backToMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).Navigate(new StartPage());
        }
    }
}