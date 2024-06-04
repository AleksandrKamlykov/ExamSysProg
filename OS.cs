using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSysProg
{


    public class OS
    {
        private ProcessManager processManager = new ProcessManager();
        private MemoryManager memoryManager = new MemoryManager();
        private FileSystem fileSystem = new FileSystem();

        public void Run()
        {
            while (true)
            {
                System.Console.WriteLine(@"
You can

create:
    -process
    -file
    -directory
show:
    -files
terminate:
    -process
suspend:
    -process
resume:
    -process
write:
    -file
read:
    -file
delete:
    -file
allocate:
    -memory
free:
    -memory
           ");
                Console.Write("> ");
                var input = Console.ReadLine();

                if (input == null)
                {
                    continue;
                }

                var command = input.Split(' ');

                switch (command[0])
                {
                    case "create":
                        if (command[1] == "process")
                        {
                            var process = processManager.CreateProcess(command[2]);
                            Console.WriteLine($"Process created: {process}");
                        }
                        else if (command[1] == "file")
                        {
                            fileSystem.CreateFile(command[2]);
                        }
                        else if (command[1] == "directory")
                        {
                            fileSystem.CreateDirectory(command[2]);
                        }
                        break;
                    case "show":
                        if (command[1] == "files")
                        {
                            fileSystem.ShowFiles();
                        }
                        break;
                    case "terminate":
                        processManager.TerminateProcess(int.Parse(command[2]));
                        Console.WriteLine($"Process {command[2]} terminated");
                        break;
                    case "suspend":
                        processManager.SuspendProcess(int.Parse(command[2]));
                        Console.WriteLine($"Process {command[2]} suspended");
                        break;
                    case "resume":
                        processManager.ResumeProcess(int.Parse(command[2]));
                        Console.WriteLine($"Process {command[2]} resumed");
                        break;
                    case "write":
                        if (command[1] == "file")
                        {
                            fileSystem.WriteFile(command[2], command[3]);
                            Console.WriteLine($"Data written to file {command[2]}");
                        }
                        break;
                    case "read":
                        if (command[1] == "file")
                        {
                            var data = fileSystem.ReadFile(command[2]);
                            Console.WriteLine($"Data from file {command[2]}: {data}");
                        }
                        break;
                    case "delete":
                        if (command[1] == "file")
                        {
                            fileSystem.DeleteFile(command[2]);
                            Console.WriteLine($"File {command[2]} deleted");
                        }
                        break;
                    case "allocate":
                        if (command[1] == "memory")
                        {
                            var address = memoryManager.AllocateMemory(int.Parse(command[2]));
                            Console.WriteLine($"Memory allocated at address {address}");
                        }
                        break;
                    case "free":
                        if (command[1] == "memory")
                        {
                            memoryManager.FreeMemory(int.Parse(command[2]));
                            Console.WriteLine($"Memory freed at address {command[2]}");
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }

}
