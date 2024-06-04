using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSysProg
{
    public class MemoryManager
    {
        private Dictionary<int, int> memoryAllocations = new Dictionary<int, int>();
        private int nextAddress = 1;

        public int AllocateMemory(int size)
        {
            var address = nextAddress;
            memoryAllocations[address] = size;
            nextAddress += size;
            return address;
        }

        public void FreeMemory(int address)
        {
            if (memoryAllocations.ContainsKey(address))
            {
                memoryAllocations.Remove(address);
            }
        }

        public Dictionary<int, int> GetAllocations()
        {
            return memoryAllocations;
        }
    }
}
