using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    [Serializable()]
    class Block
    {
        public Block(string ts, string dat, string hs, int nc)
        { timestamp = ts; data = dat; hash = hs; nonce = nc; }

        public string timestamp;
        public string data;
        public string hash;
        public int nonce;
    }
}
