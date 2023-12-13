using System;
using System.IO;
using System.IO.Compression;

namespace GzipGunzip
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length == 1) 
			{
				try{
				FileInfo fis = new FileInfo(args[0]);
				Console.WriteLine("Input file {0}",fis.FullName);
				if(string.Compare(fis.Extension,".gz") == 0)
					Uncompress(fis.FullName);
				else
					Compress(fis.FullName);
				}catch(Exception ex){
					Console.WriteLine(ex.Message);
				}
			}else
				Console.WriteLine("\nUsage: GzipGunzip filename\n");
		}

		static void Uncompress (string filename)
		{
			Console.WriteLine ("Uncompressing .... {0} ", filename);
			string destFile = filename.Substring (0, filename.Length - 3);
			FileStream inputStream = new FileStream (filename
			                                        , FileMode.Open
			                                        , FileAccess.Read);
			using (FileStream outputStream = new FileStream (destFile
			                                         , FileMode.Create
			                                         , FileAccess.Write)) {
				using(DeflateStream zipStream = new DeflateStream (inputStream
			                                             , CompressionMode.Decompress)){
				int inputByte = zipStream.ReadByte ();
				while (inputByte != -1) {
					outputStream.WriteByte ((byte)inputByte);
					inputByte = zipStream.ReadByte ();
				}
				}
			}

			Console.WriteLine("output: {0}",destFile);
		}
		static void Compress (string filename)
		{
			Console.WriteLine ("Compressing .... {0} ", filename);
			string destFile = filename + ".gz";
			byte[] buffer = null;
			using (FileStream inputStream = new FileStream(filename
			                                 ,FileMode.Open
			                                        ,FileAccess.Read)) {
				buffer = new byte[inputStream.Length];
				inputStream.Read (buffer, 0, buffer.Length);

			}
			Console.WriteLine ("bytes read: {0}", buffer.Length);
			using (FileStream outputStream = new FileStream(destFile
				                                  ,FileMode.Create
			                                         ,FileAccess.Write)) {
				using(DeflateStream zipStream = new DeflateStream (outputStream
					                            , CompressionMode.Compress)){
				zipStream.Write (buffer, 0, buffer.Length);
				}
			}
			Console.WriteLine("bytes written: {0}",buffer.Length);
		}
	}
}
