namespace RomPadder
{
	internal class Program
	{
		private const long bytes_8mb = 1024 * 1024 * 8;
		private const long bytes_16mb = 1024 * 1024 * 16;
		private const long bytes_32mb = 1024 * 1024 * 32;
		private const long bytes_64mb = 1024 * 1024 * 64;
		private const long bytes_128mb = 1024 * 1024 * 128;

		static void Main(string[] args)
		{
			string filename = @"D:\GameCarts\N64\Games\Donkey Kong 64 - Tag Anywhere\ROM\Donkey Kong 64 (Tag Anywhere).z64";

			// read rom data
			var romData = File.ReadAllBytes(filename);

			// calculate padding needed
			long paddingSize;
			if (romData.Length <= bytes_8mb) paddingSize = bytes_8mb;
			else if (romData.Length <= bytes_16mb) paddingSize = bytes_16mb;
			else if (romData.Length <= bytes_32mb) paddingSize = bytes_32mb;
			else if (romData.Length <= bytes_64mb) paddingSize = bytes_64mb;
			else if (romData.Length <= bytes_128mb) paddingSize = bytes_128mb;
			else
			{
				Console.WriteLine($"ERROR: ROM size is to large: {romData.LongLength / 1024 / 1024}-MB / {romData.LongLength}-B");
				return;
			}

			// pad rom data
			var data = new byte[paddingSize];
			Array.Copy(romData, data, romData.LongLength);

			// write padded rom file
			string filenamePadded = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + $" [Padded {paddingSize / 1024 / 1024}mb]" + Path.GetExtension(filename));
			File.WriteAllBytes(filenamePadded, data);
		}
	}
}
