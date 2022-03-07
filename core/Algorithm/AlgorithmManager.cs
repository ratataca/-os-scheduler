using System;
using System.Collections.Generic;
using System.Text;

namespace MsOSProgram.core.Algorithm
{
    class AlgorithmManager
    {
        List<Object> algorithmList;
        public int mCount = 0;
        public AlgorithmManager()
        {
            algorithmList = new List<Object>();
        }

        public void add(Object algorithm)
        {
            algorithmList.Add(algorithm);
            mCount += 1;
        }

        public Object AlgorithmUnit(int index) {
            return algorithmList[index];
        }

    }
}
