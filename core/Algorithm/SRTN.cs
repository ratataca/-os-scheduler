﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MsOSProgram.core.Algorithm
{
    class SRTN
    {
        // 개인이 가지고 있는 process list(배열) 
        //  - 현재 시간에 process가 들어온다면 여기에 담아서 앞으로 처리
        //  - 최종 결과 리턴을 위해 가지고 있음
        public ScheduleContainer scheduleContainer = new ScheduleContainer();
        bool finished = false;

        int clock = 0;  // 현재 clock -> 자동으로 1씩 증가해야 함
        
        List<Process> processList = new List<Process>();
        Process currentProcess = null;
        Process previousProcess = null;

        public int contextSwitching = 0;
        public double averageForsum = 0;
        double averageResponseTime = 0;
        int inputProcessCount = 0;

        public SRTN()
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
            // ------------------------------------------
            //                          date : 2020-05-14
            // ------------------------------------------
            // - input : 현재 arrive time에 맞는 process를 
            //           process container로부터 받음
            // - output : 알고리즘을 통해 지금 현재 처리된 process를 return 

            // ** 여기서부터 코드를 작성하세요

            // null 처리 필요함
            // - 현재 들어온 process가 없을 시 null받음
            if (process != null)
            {
                scheduleContainer.add(process);
                addProcessList(process);

                inputProcessCount++;
            }

            var resultProcess = run();

            if (resultProcess == null && processList.Count == 0)
                return null;

            return resultProcess;
        }


        public Process run()
        {
            clock++;

            if (processList.Count == 0 && currentProcess == null)
                return null;

            if (currentProcess == null)
            {
                currentProcess = processList[0];
                processList.RemoveAt(0);
            }

            currentProcess.burstTime--;

            Process process = currentProcess;

            if (currentProcess.burstTime == 0)
            {
                process = scheduleContainer.changeTT(currentProcess.processId, clock);

                // TODO
                averageForsum += process.turnAroundTime;

                contextSwitching++;

                previousProcess = currentProcess;
            }
            else
            {
                addProcessList(currentProcess);
            }

            currentProcess = null;


            //Console.WriteLine("===========================");
            //Console.WriteLine("=== Current Clock " + clock.ToString());
            //Console.WriteLine("===========================");
            //Console.WriteLine("Current Working Process : " + currentProcess.processId.ToString());
            //Console.WriteLine("Arrive Time : " + currentProcess.arrTime.ToString());
            //Console.WriteLine("Burst Time : " + currentProcess.burstTime.ToString());
            //Console.WriteLine("Context Switching : " + contextSwitching.ToString());
            //Console.WriteLine("===========================");
            //Console.WriteLine();
            //Console.WriteLine();


            return process;
        }

        public void addProcessList(Process newProcess)
        {
            bool isInserted = false;

            if (newProcess == null)
                return;

            if (newProcess.processId == "p4")
                Console.WriteLine();

            int newProcessBT = newProcess.burstTime;


            if (processList.Count != 0)
            {
                if (newProcessBT < processList[0].burstTime)
                {
                    isInserted = true;
                    processList.Insert(0, newProcess);
                }
                
                else
                {
                    for (int i = 0; i < processList.Count - 1; i++)
                    {
                        if (processList[i].burstTime <= newProcessBT && newProcessBT < processList[i + 1].burstTime)
                        {
                            isInserted = true;
                            processList.Insert(i + 1, newProcess);
                            break;
                        }
                    }
                }
            }

            if (isInserted == false)
                processList.Add(newProcess);
        }

        // 만지지마요! .
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
