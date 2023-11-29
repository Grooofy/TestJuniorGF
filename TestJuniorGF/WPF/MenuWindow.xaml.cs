﻿using System.Windows;

namespace TestJuniorGF
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void GoPlay(object sender, RoutedEventArgs routedEventArgs)
        {
            var game = new GameWindow
            {
                Top = Top,
                Left = Left
            };
            game.Show();
            Close();
        }
    }
}