using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;

string? readResult;
string menuSelection = "";
string filename ="";
string? s;

do
{

		Console.Clear();
		Console.WriteLine("Welcome to the Gzip sample. Your main menu options are:");
		Console.WriteLine(" 1. Compress");
		Console.WriteLine(" 2. Uncompress");
		Console.WriteLine();
		Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
		readResult = Console.ReadLine();
		if (readResult != null)
		{
			menuSelection = readResult.ToLower();	
			if (menuSelection == "exit")
			break;
			
			do
			{
					Console.Write("Enter filename: ");
					s = Console.ReadLine();
					if(!String.IsNullOrEmpty(s))
					{
						filename = s;
						switch (menuSelection)
						{
						case "1":
							Compress(filename);
							Console.WriteLine("Press enter to continue.");
							Console.ReadLine();
						break;
						case "2":
							Uncompress(filename);
							Console.WriteLine("Press enter to continue.");
							Console.ReadLine();
						break;
						default:
							break;
						}
					}
			}while(string.IsNullOrEmpty(s));
					
		}
} while(menuSelection != "exit");






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
			byte[]? buffer = null;
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

