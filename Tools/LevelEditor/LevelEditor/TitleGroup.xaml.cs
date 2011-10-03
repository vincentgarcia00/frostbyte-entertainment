using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for TitleGroup.xaml
    /// </summary>
    public partial class TileGroup : UserControl
    {
        public string GroupName { get; set; }

        public TileGroup()
        {
            this.InitializeComponent();
        }

        public TileGroup(ObservableCollection<Tile> tiles)
        {
            this.InitializeComponent();
            // TODO: Complete member initialization
            Tiles.SelectionChanged += new SelectionChangedEventHandler(Tiles_SelectionChanged);

            Tiles.ItemsSource = tiles;
        }

        void Tiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            This.mainWindow.SelectedTile = Tiles.SelectedValue as Tile;
        }
    }
}
