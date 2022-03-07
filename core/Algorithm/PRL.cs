using System;
using System.Collections.Generic;
using System.Text;

namespace MsOSProgram.core.Algorithm
{
    class PRL
    {
        // 개인이 가지고 있는 process list(배열) 
        //  - 현재 시간에 process가 들어온다면 여기에 담아서 앞으로 처리
        //  - 최종 결과 리턴을 위해 가지고 있음
        public ScheduleContainer scheduleContainer = new ScheduleContainer();
        bool finished = false;

        int clock = 0;  // 현재 clock -> 자동으로 1씩 증가해야 함
        
        
        Queue<Process> queueProcess1 = new Queue<Process>();        // 가중치가 더 높은 애들
        Queue<Process> queueProcess2 = new Queue<Process>();        // 가중치가 낮은 애들

        /////////////////////////////////////////// 변수 선언 ///////////////////////////////////////////

        // 몇번 p1이 실시 되었는지
        public int queueProcess1PeekCount = 0;

        // 각각의 뽑는 비율
        const int q1LotteryRatio = 8;
        const int q2LotteryRatio = 2;
        const int leastCallProcess = 3;

        public int contextSwitching = 0;
        public double averageForsum = 0;
        double averageResponseTime = 0;
        int inputProcessCount = 0;

        // 랜덤 객체 생성
        Random rand = new Random();


        public PRL()
        {
            // 생성자 - 만지지마요!            
        }

        public void initClock()
        {
            clock = 0;

            contextSwitching = 0;
            averageForsum = 0;
            averageResponseTime = 0;
            inputProcessCount = 0;
        }

        public Process run_per_1s(Process process)
        {
            if (process != null)
            {
                scheduleContainer.add(process);

                // 가중치 측정 랜덤 함수..
                int weight = process.burstTime;

                if (weight > 5)
                {
                    queueProcess1.Enqueue(process);
                }
                else
                {
                    queueProcess2.Enqueue(process);
                }

                inputProcessCount++;
            }

            return run();
        }

        
        public Process run()
        {
            clock++;

            Process process = null;
            int choiceQ = -1;

            if (queueProcess1.Count == 0 && queueProcess2.Count == 0)
            {
                // 둘다 비어있으면 반응 없음
                return null;
            }

            // queue1에 process가 존재하며 isQueueProcess1PeekOk()을 고려하여 어떤 것을 고를지 선택
            //   - isQueueProcess1PeekOk() : P2에 process가 존재하는 횟수가 q2LotteryRatio의 비율을 안 넘었는지
            if (queueProcess1.Count > 0 && queueProcess2.Count > 0)
            {

                //choiceQueueUsedRandomWeight   
                //   - 1 ~ q2LotteryRatio 사이 값이 나오면         true  => p2 선택
                //   - (q2LotteryRatio+1) ~ 10 사이 값이 나오면    false => p1 선택
                if (choiceQueueUsedRandomWeight())
                { //true => p2 선택
                    process = queueProcess2.Peek();
                    choiceQ = 2;
                }
                else
                { //오면 false => p1 선택

                    // 아직 1을 기다려도 되는지
                    if (isQueueProcess1PeekOk())     // 아직 1을 뽑아도 괜찮다
                    {
                        process = queueProcess1.Peek();
                        choiceQ = 1;
 
                    }
                    else                            // 안된다 2를 한번이라도 선택해야한다
                    {
                        process = queueProcess2.Peek();
                        choiceQ = 2;

                    }
                }
            }
            else if (queueProcess1.Count > 0 && queueProcess2.Count == 0)
            {
                process = queueProcess1.Peek();
                choiceQ = 1;
            }
            else if (queueProcess1.Count == 0 && queueProcess2.Count > 0)
            {
                process = queueProcess2.Peek();
                choiceQ = 2;
            }
            /////////////////////////////////////////// 전처리 끝 ///////////////////////////////////////////

            if (process.burstTime > 0) process.burstTime--;
            if (process.burstTime == 0)
            {
                process = scheduleContainer.changeTT(process.processId, clock);

                // TODO
                averageForsum += process.turnAroundTime;

                if (choiceQ == 1)
                    queueProcess1.Dequeue();
                else
                    queueProcess2.Dequeue();
                
            }
            return process;
        }

        // 이건 최종적으로 모든 프로세스가 끝나면 메인에서 부를 것들,,,
        public Dictionary<string, string> getAllResult()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (inputProcessCount != 0)
            {
                averageResponseTime = averageForsum / inputProcessCount;
            }
            else {
                averageResponseTime = 0;
            }
            
            // 연산이 끝난 후 알고리즘 수행 결과에 대한 내용
            // key 값은 고정해주시고 value 값을 입력해주세요 (string type)
            result.Add("AVERAGE_WAITING_TIME", averageResponseTime.ToString());
            result.Add("CONTEXT_SWITCHING", contextSwitching.ToString());

            return result;
        }

        public bool check(string processId, int arrTime, int burstTime, int watingTime, int turnAroundTime, double normalizedTT)
        {
            return scheduleContainer.check(processId, arrTime, burstTime, watingTime, turnAroundTime, normalizedTT);
        }

        public string changeBt()
        {
            //scheduleContainer.
            return "";
        }

        
        public bool isQueueProcess1PeekOk()
        {
            if (queueProcess1PeekCount >= leastCallProcess)
            {
                // p2 에게 양보
                queueProcess1PeekCount = 0;
                return false;
            }
            // 아직 p1 괜찮
            queueProcess1PeekCount++;
            return true;
        }

        public bool choiceQueueUsedRandomWeight()
        {
            // 가중치를 포함한 랜덤 - 하나뽑기 
            //   - 1 ~ q2LotteryRatio 사이 값이 나오면 true => p2 선택
            //   - (q2LotteryRatio+1) ~ 10 사이 값이 나오면 false => p1 선택

            int weight = rand.Next(1, 11); // 최소 min 값 부터 최대 max - 1 까지

            if (weight <= q2LotteryRatio)
                return true;

            return false;
        }


    }
}
