using MsOSProgram.ui.layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsOSProgram.core.Algorithm
{
    class RR
    {
        // 개인이 가지고 있는 process list(배열) 
        //  - 현재 시간에 process가 들어온다면 여기에 담아서 앞으로 처리
        //  - 최종 결과 리턴을 위해 가지고 있음
        public ScheduleContainer scheduleContainer = new ScheduleContainer();
        bool finished = false;

        int timeQuantum = 2;

        int clock = 0;  // 현재 clock -> 자동으로 1씩 증가해야 함
        int rrClock = 0;

        public int contextSwitching = 0;
        public double averageForsum = 0;
        double averageResponseTime = 0;
        int inputProcessCount = 0;

        Queue<Process> queueProcess = new Queue<Process>();

        public RR()
        {
            // 생성자 - 만지지마요!            
        }

        public void timeQuantumInit() {
            //timeQuantum;
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
            // ------------------------------------------
            //                          date : 2020-05-14
            // ------------------------------------------
            // - input : 현재 arrive time에 맞는 process를 
            //           process container로부터 받음
            // - output : 알고리즘을 통해 지금 현재 처리된 process를 return 

            // ** 여기서부터 코드를 작성하세요

            // null 처리 필요함
            // - 현재 들어온 process가 없을 시 null받음
            timeQuantum = WorkSpace.time_quantum;

            if (process != null)
            {
                scheduleContainer.add(process);
                queueProcess.Enqueue(process);

                inputProcessCount++;
            }

            return run();
        }


        public Process run()
        {
            clock++;

            if (queueProcess.Count == 0)
                return null;

            Process process = queueProcess.Peek();

            if (process.burstTime > 0 && rrClock == timeQuantum)
            {
                queueProcess.Dequeue();
                queueProcess.Enqueue(process);
                contextSwitching++;

                rrClock = 0;
            }

            process = queueProcess.Peek();

            if (process.burstTime > 0 && rrClock < timeQuantum)
            {
                process.burstTime--;
                rrClock++;
            }

           

            if (process.burstTime == 0)
            {
                process = scheduleContainer.changeTT(process.processId, clock);

                // TODO
                averageForsum += process.turnAroundTime;

                queueProcess.Dequeue();
                rrClock = 0;

            }

            return process;
        }

        // 만지지마요! 
        public bool isFinished()
        {
            return finished;
        }

        // 이건 최종적으로 모든 프로세스가 끝나면 메인에서 부를 것들,,,
        public Dictionary<string, string> getAllResult()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (inputProcessCount != 0)
            {
                averageResponseTime = averageForsum / inputProcessCount;
            }
            else
            {
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
    }
}
