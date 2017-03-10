using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EnergetskiPregled.Common.Helpers
{
	public static class FileHelper
	{
		public static string SHA1Hash(string path)
		{
			string hash = "";
			using (FileStream stream = File.OpenRead(path))
			{
				var sha1 = SHA1.Create();
				byte[] checksum = sha1.ComputeHash(stream);
				hash = BitConverter.ToString(checksum).Replace("-", string.Empty);
			}
			return hash;
		}
	}
}
