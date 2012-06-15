<Query Kind="Program">
  <Reference Relative="NAudio.dll">NAudio.dll</Reference>
  <Namespace>NAudio.Wave</Namespace>
</Query>

void Main()
{
	var path = @"C:\Users\roverby\Desktop\ch";
	var di = new DirectoryInfo(path);
	var mp3s = di.GetFiles("*.mp3");
	var outputPath = Path.Combine(path,"joined.mp3");
	Join(mp3s.OrderBy(x => x.Name).Select (x => x.FullName).ToArray(), outputPath);
}

void Join(string[] mp3s, string outputPath)
{	
	using (var writer = File.Create(outputPath))
	{	
		foreach (var mp3 in mp3s)
		{
			using (var mp3Reader = new Mp3FileReader(mp3))
			{
				Mp3Frame frame;
				while ((frame = mp3Reader.ReadNextFrame()) != null)
				{					
					writer.Write(frame.RawData, 0, frame.RawData.Length);
				}
			}
		}
	}
}