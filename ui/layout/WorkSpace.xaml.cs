using MsOSProgram.ui.components.Common;
using MsOSProgram.ui.components.Workspace;
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

namespace MsOSProgram.ui.layout
{
    /// <summary>
    /// WorkSpace.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WorkSpace : UserControl
    {
        public int processId = 0;
        public int processMarginTop = 0;
        public int grapeMarginTop = 0;
        List<string> processAtList = new List<string>();

        public static int time_quantum = -1;

        int len = 0;
        
        //PlaygroundGraph[] GraphBox = new PlaygroundGraph[4];
        List<ProcessTableitem> processTableItemList = new List<ProcessTableitem>();
        
        public WorkSpace()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent StartBtnRouteEvent =
            EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(WorkSpace)
                );
        public event RoutedEventHandler StartBtnValueChangedEventHandler
        {
            add
            {
                AddHandler(StartBtnRouteEvent, value);
            }
            remove
            {
                RemoveHandler(StartBtnRouteEvent, value);
            }
        }
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            numericUpDownCPU.Visibility = Visibility.Hidden;
            TextSystemParmeter.Text = time_quantum.ToString();
            TextSystemParmeter.Visibility = Visibility.Visible;

            this.RaiseEvent(
                new BtnStartRouteEvent (
                    StartBtnRouteEvent,
                     Convert.ToInt32(time_quantum)
                    )
                );
        }

        public void getInformation()
        {

            return;
        }

        private void numericUpDownCPU_ValueChanged(object sender, ControlLib.ValueChangedEventArgs e)
        {
            
            time_quantum = Convert.ToInt32((e.NewValue.ToString()));

        }
        
        public List<ProcessTableitem> getProcessTableItemList()
        {
            // 다른 곳에서 ProcessTable요청

            return processTableItemList;
        }

        #region ADD 버튼 클릭 이벤트 처리
        private void BtnATBTAdd_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;

            string AT = TextBoxATINPUT.Text;
            string BT = TextBoxBTINPUT.Text;

            // 동일한 AT막기
            for (int i = 0; i < processAtList.Count; i++) {
                if (processAtList[i] == AT) {
                    check = true;
                }
            }

            // 동일 시간에 또 프로세스가 들어오기
            if ((AT == "0" && BT == "0") || BT == "0")
            {
                GridError.Visibility = Visibility.Visible;
                GridError.Text = "잘못된 입력 형식입니다.";
            }
            else if (Convert.ToInt32(AT) < 0) {
                GridError.Visibility = Visibility.Visible;
                GridError.Text = "음수는 입력하실 수 없습니다.";
            }
            else if (check)
            {
                GridError.Visibility = Visibility.Visible;
                GridError.Text = "동일한 AT가 이미 있습니다.";
            }
            else
            {
                GridError.Visibility = Visibility.Hidden;
                check = false;

                processAtList.Add(AT);
                GridError.Text = "";
                // UI를 위한 작업
                ProcessTableitem work = new ProcessTableitem()
                {
                    Width = 300,
                    Height = 60,
                    Margin = new Thickness(0, processMarginTop, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                };

                processId += 1;
                processMarginTop += 61;

                string processIdRename = "p" + processId.ToString();
                work.init(processIdRename, TextBoxATINPUT.Text, TextBoxBTINPUT.Text);

                //GridProcessTable
                GridProcessTable.Children.Add(work);

                // 리스트로 처리
                //   - 리스트로 받아내기 위한
                processTableItemList.Add(work);

                //   - 프로세스의 수
                len = processTableItemList.Count;

                

                // 실제 코딩을 위한 작업
            }
        }
        #endregion

        #region DELETE 버튼 클릭 이벤트 처리
        private void BtnATBTDelete_Click(object sender, RoutedEventArgs e)
        {
            processAtList = new List<string>();

            if (GridProcessTable.Children.Count > 0 && len > 0) 
            {

                // 실제 배열에 있는 값을 통해 삭제하기
                
                GridProcessTable.Children.Clear();

                // - 삭제에 따른 변수 초기화
                processId = 0;
                processMarginTop = 0;
                len = 0;

                // - 리스트 삭제
                processTableItemList = new List<ProcessTableitem>();
            }
            else {
                ;//Console.WriteLine(" >>> Input값이 없습니다.");
            }
        }
        #endregion

        private void TextBoxATINPUT_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToInt32(TextBoxATINPUT.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("숫자만 입력해주세요");
                return;
            }
        }

        private void TextBoxBTINPUT_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToInt32(TextBoxBTINPUT.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("숫자만 입력해주세요");
                return;
            }
        }
    }
}
