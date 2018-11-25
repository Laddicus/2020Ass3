using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Program
    {
        class Node
        {
            public string Directory { get; set; }
            public List<string> File { get; set; }
            public Node LeftMostChild { get; set; }
            public Node RightSibling { get; set; }

            public Node(string directory, List<string> file, Node leftMostChild, Node rightSibling)
            {
                Directory = directory;
                File = file;
                LeftMostChild = leftMostChild;
                RightSibling = rightSibling;
            }
            public Node(string directory)
            {
                Directory = directory;
                File = new List<string>();
                LeftMostChild = null;
                RightSibling = null;
            }
        }
        class FileSystem
        {
            public Node Root;
            int num = 0;
            // Creates a file system with a root directory
            public FileSystem(string start)
            {
                Root = new Node(start);
            }
            // Adds a file at the given address
            // Returns false if the file already exists or the path is undefined; true otherwise
            public bool AddFile(string address)
            {
                Node curr = Navigate(address, Root);


                if (curr.Directory == address)
                {
                    Console.WriteLine("What do you want it to be named?");
                    curr.File.Add(Console.ReadLine());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // Removes the file at the given address
            // Returns false if the file is not found or the path is undefined; true otherwise
            public bool RemoveFile(string address)
            {
                string directory = address.Remove(address.Length - 1);
                string file = address[address.Length - 1].ToString();
                
                Node curr = Navigate(directory, Root);
                if (curr.Directory == directory)
                {
                    curr.File.Remove(file);

                }
                return false;
            }
            // Adds a directory at the given address
            // Returns false if the directory already exists or the path is undefined; true otherwise
            public bool AddDirectory(string address)
            {
                Node curr = Navigate(address, Root);
                if (curr.Directory == address)
                {
                    if(curr.LeftMostChild == null)
                    {
                        Console.WriteLine("What do you want it to be named");
                        curr.LeftMostChild = new Node(curr.Directory + Console.ReadLine());
                    }
                    else
                    {
                        Node temp = curr.LeftMostChild;
                        while(temp.RightSibling != null)
                        {
                            temp = temp.RightSibling;
                        }
                        Console.WriteLine("What do you want it to be named");
                        temp.RightSibling = new Node(curr.Directory + Console.ReadLine());
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            // Removes the directory (and its subdirectories) at the given address
            // Returns false if the directory is not found or the path is undefined; true otherwise
            public bool RemoveDirectory(string address)
            {
                string parent = address.Remove(address.Length - 1);
                
                Node curr = Navigate(parent, Root);
                if (curr.LeftMostChild.Directory == address)
                {
                    curr.LeftMostChild = curr.LeftMostChild.RightSibling;
                    return true;
                }
                else
                    return false;
            }
            // Returns the number of files in the file system
            public int NumberFiles(Node curr)
            {
                num++;

                NumberFiles(curr.RightSibling);

                NumberFiles(curr.LeftMostChild);

                return num;
            }
            // Prints the directories in a pre-order fashion along with their files
            public void PrintFileSystem(Node curr )
            {
                if (curr == null)
                    return;

                Console.WriteLine(curr.Directory);
                foreach (string s  in curr.File)
                {
                    Console.Write("-{0} ", s);
                }

                PrintFileSystem(curr.RightSibling);

                PrintFileSystem(curr.LeftMostChild);
            }
            public Node Navigate(string address, Node curr)
            {
                if (curr == null)
                    return curr;
                if (address == curr.Directory)
                {
                    return curr;
                }
                else
                {
                    if (curr.RightSibling != null)
                        curr = Navigate(address, curr.RightSibling);

                    if (curr.LeftMostChild != null)
                        curr = Navigate(address, curr.LeftMostChild);
                   return curr;
                }
            }
        }
    
        static void Main()
        {
            Console.WriteLine("Input root");
            FileSystem Sys = new FileSystem(Console.ReadLine());
            while (true)
            {
                Console.WriteLine("Make a selection:\n1. Add File\n2. Remove File\n3. Add Directory\n4. Remove Directory\n5. Find Number of Files\n6. Print File System\n7. Exit");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("At what address do you want to add a file?");
                        Sys.AddFile(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine("What is the address of the file you want to delete?");
                        Sys.RemoveFile(Console.ReadLine());
                        break;
                    case 3:
                        Console.WriteLine("At what address would you like to add a directory?");
                        Sys.AddDirectory(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("What is the address of the directory you want to delete?");
                        Sys.RemoveDirectory(Console.ReadLine());
                        break;
                    case 5:
                        Sys.NumberFiles(Sys.Root);
                        break;
                    case 6:
                        Sys.PrintFileSystem(Sys.Root);
                        break;
                    case 7:
                        return;
                }
            }
        }
    }
}
