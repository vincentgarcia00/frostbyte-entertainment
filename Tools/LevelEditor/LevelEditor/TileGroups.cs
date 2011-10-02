using System;
using System.Collections.Generic;
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

namespace LevelEditor
{
    public partial class TileGroups : UserControl
    {
        public List<Tile> Tiles { get; set; }

        public string GroupName { get; set; }

        public TileGroups()
        {
            this.InitializeComponent();
        }
    }
}
