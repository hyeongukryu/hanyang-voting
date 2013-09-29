using HanyangVoting.Clients.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceImplementations
{
    class DefaultBinaryHashComputer : IBinaryHashComputer
    {
        public string ComputeHash()
        {
            var binaryData = GetBinaryData();
            return ComputeHashString(binaryData);
        }

        private byte[] GetBinaryData()
        {
            List<string> assemblies = new List<string>(
                new[] { "HanyangVoting.dll", "HanyangVoting.Clients.dll" });

            var entryAssembly = Assembly.GetEntryAssembly().Location;
            assemblies.Add(entryAssembly);

            List<byte> bytes = new List<byte>();

            foreach (var location in assemblies.OrderBy(l => l))
            {
                bytes.AddRange(File.ReadAllBytes(location));
            }

            return bytes.ToArray();
        }

        private byte[] ComputeHash(byte[] input)
        {
            return SHA1.Create().ComputeHash(input);
        }

        private string ComputeHashString(byte[] input)
        {
            var hash = ComputeHash(input);
            var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return hashString;
        }
    }
}
