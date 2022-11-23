using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blockchain
{
    static class Blockchain
    {
        public static List<Block> blockchain = new List<Block>();
        static int difficulity = 2;
        public static void AddBlock(string dat = "genesis", string prvHash = "") //В эту функцию передаются все данные для создания блока 
        {
            int nonce = 0; //Число, которое будет менять блокчейн для соответствия сложности
            string timestamp = Convert.ToString(DateTime.Now);
            while (true)
            {

                string newHash = getHash(timestamp, dat, prvHash, nonce); //Вычисляем хэш, дополнительно передавая число сложности

                if (newHash.StartsWith(String.Concat(Enumerable.Repeat("0", difficulity))))
                {
                    Console.WriteLine($"found {newHash}, nonce - {nonce}");
                    blockchain.Add(new Block(timestamp, dat, newHash, nonce));

                    break;
                }
                else //Иначе - считать со следующим значением nonce
                {
                    nonce++;
                }
            }

        }

        public static void EDIT(int block, string data)
        {
            blockchain[block].data = data;
        }

        public static void Verification()
        {
            for (int i = 1; i != blockchain.Count; i++)
            {
                string verHash = getHash(blockchain[i].timestamp, blockchain[i].data, blockchain[i - 1].hash, blockchain[i].nonce);
                if (verHash == blockchain[i].hash)
                {
                    Console.WriteLine($"Block {i} - OK");
                }
                else
                {
                    Console.WriteLine($"cant confirm block {i}!!!");
                    return;
                }

            }
            Console.WriteLine("All blocks are confirmed");
        }

        static BinaryFormatter formatter = new BinaryFormatter();
        public static void SaveBlockchain(string filename)
        {

            using (FileStream fs = new FileStream(filename + ".bogdan", FileMode.Create))
            {

                formatter.Serialize(fs, blockchain);
            }
        }

        public static void LoadBlockhain(string filename)
        {
            using (FileStream fs = new FileStream(filename + ".bogdan", FileMode.OpenOrCreate))
            {
                try
                {
                blockchain = (List<Block>)formatter.Deserialize(fs);
                
                foreach (Block blc in blockchain)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", blc, blc.data, blc.hash, blc.timestamp);
                }
                }
                catch { Console.WriteLine("U NAS UTECHKA"); }
            }



        }

        static string getHash(string ts, string dat, string prvHash, int nonce)
        {

            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                    .ComputeHash(Encoding.UTF8.GetBytes(ts + dat + prvHash + nonce))
                    .Select(item => item.ToString("x2"))); ;
            }
        }
    }
}
