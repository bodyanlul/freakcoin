﻿namespace Blockchain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hi");
            Blockchain.AddBlock();
            while (true)
            {

                string cmd = Console.ReadLine();
                if (cmd == "newblock")
                {
                    Console.Write("Данные на запись: ");
                    string dat = Console.ReadLine();
                    string prevHash = Blockchain.blockchain.Last().hash;
                    Blockchain.AddBlock(dat, prevHash);
                }
                else if (cmd == "history")
                {
                    int i = 0;
                    foreach (Block blc in Blockchain.blockchain)
                    {
                        Console.WriteLine($"{i}, {blc.data}, {blc.hash}, {blc.timestamp}, {blc.nonce}");
                        i++;
                    }
                }
                else if (cmd == "verify")
                {
                    Blockchain.Verification();
                }
                else if (cmd == "edit")
                {
                    int block = Convert.ToInt32(Console.ReadLine());
                    string newData = Console.ReadLine();
                    Blockchain.EDIT(block, newData);
                }
                else if (cmd == "save")
                {
                    Console.Write("Filename: ");
                    string filename = Console.ReadLine();
                    Blockchain.SaveBlockchain(filename);
                }
                else if (cmd == "load")
                {
                    Console.Write("Filename: ");
                    string filename = Console.ReadLine();
                    Blockchain.LoadBlockhain(filename);
                }
            }
        }
    }
}