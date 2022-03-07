using MsOSProgram.core.Algorithm;
using MsOSProgram.ui.components.Common;
using MsOSProgram.ui.components.Workspace;
using MsOSProgram.ui.route_event;
using MsOSProgram.ui.unit.Playground.Sheets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Xps.Serialization;

namespace MsOSProgram.ui.layout
{
    /// <summary>
    /// PlayGround.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayGround : UserControl
    {
        System.Timers.Timer timerTick;

        //const string TAB_ITEM_SUMMARY = "SUMMARY";
        const string TAB_ITEM_FCFS = "FCFS";
        const string TAB_ITEM_RR = "RR";
        const string TAB_ITEM_SPN = "SPN";
        const string TAB_ITEM_SRTN = "SRTN";
        const string TAB_ITEM_HRRN = "HRRN";
        const string TAB_ITEM_MS = "MS";
        string currentTab = TAB_ITEM_FCFS;

   
        // 나의 자료구조
        // 1. 원본 데이터 - 만지지마셈
        ProcessContainer processOriginalDataContaliner;
        // 2. cpu각각에 대한 

        
        //  내가 가지고 있는 전체 알고리즘
        AlgorithmManager algorithmManager;
        // 알고리즘 결과가 들어가는것
        List<AlgorithmResult> listAlgorithmResultSheet;
        
        public int time = 0;
        public int processReturnIndex = 0;
        public int processCount = 0 ;

        // TODO - 임시 
        List<SolidColorBrush> colorTable = new List<SolidColorBrush>();
        public static int nowTimeQuantum;


        public PlayGround()
        {
            InitializeComponent();
           

            

            

            // 알고리즘이 리스트로 들어있는 객체
            algorithmManager = new AlgorithmManager();
            // Add into Algorithm Manager
            // 연산 알고리즘이 들어있는 곳
            algorithmManager.add(new FCFS());
            algorithmManager.add(new RR());
            algorithmManager.add(new SPN());
            algorithmManager.add(new SRTN());
            algorithmManager.add(new HRRN());
            algorithmManager.add(new PRL());

            // 실제 sheet ui들이 리스트로 들어있는 객체
            listAlgorithmResultSheet = new List<AlgorithmResult>();

            //각각의 sheet들에게 해당 알고리즘을 가지게 함
            for (int i = 0; i < algorithmManager.mCount; i++) {
                
                listAlgorithmResultSheet.Add(
                    new AlgorithmResult(
                        1,                                          // cpu
                        algorithmManager.AlgorithmUnit(i)           
                        )
                    );
            }

            // 첫 화면에 요약 띠우기
            tabItemClickPressed(TAB_ITEM_FCFS);

            // 전체 sheet에 cpu 그리기
            // TODO - cpu몇개 사용할지에 따른 선언 방식을 달리 해야함
            //for (int i = 0; i < ResultSheetManager.Count; i++)
            //{
            //    ResultSheetManager[i].addCpuVisualization();
            //    //alg.addCpuVisualization();
            //}

        }

        public void PreStartButton(List<ProcessTableitem> processTableIte, int _nowTimeQuantum) {
            // Input : UI Process Table
            nowTimeQuantum = _nowTimeQuantum;

            foreach (var sheet in listAlgorithmResultSheet)
                if (sheet.isSimulationOn == true)
                {
                    MessageBox.Show("시뮬레이션이 실행중입니다");
                    return;
                }
            
            GridResultSheets.Content = null;
            tabItemClickPressed(currentTab);

            // 알고리즘 관련 객체 선언
            processOriginalDataContaliner = new ProcessContainer();

            // processTableItem에 있는 리스트들을 실제 process로 만들기에서 Container에 원본 데이터 저장하기
            for (int i = 0; i < processTableIte.Count; i++) {
                Process pro = new Process(
                    processTableIte[i].proId, 
                    processTableIte[i].AT, 
                    processTableIte[i].BT
                    );

                processOriginalDataContaliner.add(pro);   
            }
            
            // AT기준으로 sort()
            processOriginalDataContaliner.sorted();

            for (int i = 0; i < listAlgorithmResultSheet.Count; i++) {
                listAlgorithmResultSheet[i].StartBtnClikAlgoritm(processOriginalDataContaliner);
            }
        } 
        

        #region UI - Button Event
        private void tabItemSummary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //tabItemClickPressed(TAB_ITEM_SUMMARY);
        }

        private void tabItemAlgFCFS_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabItemClickPressed(TAB_ITEM_FCFS);
        }

        private void tabItemAlgRR_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabItemClickPressed(TAB_ITEM_RR);
        }

        private void tabItemAlgSPN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabItemClickPressed(TAB_ITEM_SPN);
        }

        private void tabItemAlgSPTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabItemClickPressed(TAB_ITEM_SRTN);
        }

        private void tabItemAlgHRRN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabItemClickPressed(TAB_ITEM_HRRN);
        }

        private void tabItemAlgMS_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabItemClickPressed(TAB_ITEM_MS);
        }

        private void tabItemClickPressed(string clickType)
        {
            currentTab = clickType;

            tabItemClickUnpressed();

            var color = Color.FromArgb(255, 255, 255, 255);

            // 기존 ui 삭제
            //GridResultSheets.Children.Clear();
            
            switch (clickType)
            {
                //case TAB_ITEM_SUMMARY:
                //    tabItemSummary.Background = new SolidColorBrush(color);

                //    GridResultSheets.Content = summary;
                //    break;

                case TAB_ITEM_FCFS:
                    tabItemAlgFCFS.Background = new SolidColorBrush(color);
                    dynamic FCFS = listAlgorithmResultSheet[0];

                    GridResultSheets.Content = FCFS;
                    break;

                case TAB_ITEM_RR:
                    tabItemAlgRR.Background = new SolidColorBrush(color); 
                    dynamic RR = listAlgorithmResultSheet[1];
                    GridResultSheets.Content = RR;

                    dynamic alg = Convert.ChangeType(algorithmManager.AlgorithmUnit(1), algorithmManager.AlgorithmUnit(1).GetType());
                    //alg.timeQuantumInit(nowTimeQuantum);
                    
                    break;
                
                case TAB_ITEM_SPN:
                    tabItemAlgSPN.Background = new SolidColorBrush(color);
                    dynamic SPN = listAlgorithmResultSheet[2];
                    GridResultSheets.Content = SPN;
                    break;

                case TAB_ITEM_SRTN:
                    tabItemAlgSPTN.Background = new SolidColorBrush(color);
                    dynamic SRTN = listAlgorithmResultSheet[3];
                    GridResultSheets.Content = SRTN;
                    break;

                case TAB_ITEM_HRRN:
                    tabItemAlgHRRN.Background = new SolidColorBrush(color);
                    dynamic HRRN = listAlgorithmResultSheet[4];
                    GridResultSheets.Content = HRRN;
                    break;

                case TAB_ITEM_MS:
                    tabItemAlgPRL.Background = new SolidColorBrush(color);
                    dynamic PRL = listAlgorithmResultSheet[5];
                    GridResultSheets.Content = PRL;
                    break;
            }
        }

        private void tabItemClickUnpressed()
        {
            //tabItemSummary.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            tabItemAlgFCFS.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            tabItemAlgRR.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            tabItemAlgSPN.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            tabItemAlgSPTN.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            tabItemAlgHRRN.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            tabItemAlgPRL.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
        #endregion
    }
}
