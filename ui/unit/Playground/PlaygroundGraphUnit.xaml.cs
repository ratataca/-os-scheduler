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

namespace MsOSProgram.ui.components.Common
{
    /// <summary>
    /// PlaygroundGraphPart.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlaygroundGraphPart : UserControl
    {
        public PlaygroundGraphPart()
        {
            InitializeComponent();
        }

        // ToDo 마진 계산
        public void init(string id, int time, SolidColorBrush brush)
        {
            GridGrapWidthName.Text = time.ToString();
            GridGrapWidth.Background = brush;
        }
    }
}
