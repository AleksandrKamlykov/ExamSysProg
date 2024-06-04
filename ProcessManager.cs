using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSysProg
{
    public enum ProcessState
    {
        Running,
        Suspended,
        Terminated
    }

    public class Process
    {
        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; }
        public ProcessState State { get; set; }
        public int Priority { get; set; }

        public Process(string name, int priority = 0)
        {
            Id = nextId++;
            Name = name;
            State = ProcessState.Running;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"Process Id: {Id}, Name: {Name}, State: {State}, Priority: {Priority}";
        }
    }
    public class ProcessManager
    {
        private List<Process> processes = new List<Process>();

        public Process CreateProcess(string name, int priority = 0)
        {
            var process = new Process(name, priority);
            processes.Add(process);
            return process;
        }

        public void TerminateProcess(int id)
        {
            var process = processes.FirstOrDefault(p => p.Id == id);
            if (process != null)
            {
                process.State = ProcessState.Terminated;
            }
        }

        public void SuspendProcess(int id)
        {
            var process = processes.FirstOrDefault(p => p.Id == id);
            if (process != null)
            {
                process.State = ProcessState.Suspended;
            }
        }

        public void ResumeProcess(int id)
        {
            var process = processes.FirstOrDefault(p => p.Id == id);
            if (process != null)
            {
                process.State = ProcessState.Running;
            }
        }

        public List<Process> GetProcesses()
        {
            return processes;
        }
    }

}
