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

namespace MsOSProgram.ui.unit.Playground.Table
{
    /// <summary>
    /// ProcessResultTableItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProcessResultTableItem : UserControl
    {
        public ProcessResultTableItem()
        {
            InitializeComponent();
        }

        public void setItemContent(string pid, string at, string bt, string wt, string tt, string ntt)
        {
            txtBlockPid.Text = pid;
            txtBlockAT.Text = at;
            txtBlockBT.Text = bt;
            txtBlockWT.Text = wt;
            txtBlockTT.Text = tt;
            txtBlockNTT.Text = ntt;
        }
    }
}
