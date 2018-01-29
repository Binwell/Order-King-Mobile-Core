using System.Security.Cryptography;
using System.Text;

namespace OrderKingCoreDemo.DAL.Helpers {
	public static class Md5Helper {
		public static string Hash(string source) {
			var sourceBytes = Encoding.UTF8.GetBytes(source);
			var bytes = MD5.Create().ComputeHash(sourceBytes);
			return Encoding.UTF8.GetString(bytes);
		}
	}
}