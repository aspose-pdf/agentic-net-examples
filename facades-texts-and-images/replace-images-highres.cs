using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string highResFolder = "highres";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"High‑resolution image folder not found: {highResFolder}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            for (int pageIndex = 1; pageIndex <= document.Pages.Count; pageIndex++)
            {
                Page page = document.Pages[pageIndex];
                Aspose.Pdf.XImageCollection images = page.Resources.Images;
                int imageCount = images.Count;

                for (int imageIndex = 1; imageIndex <= imageCount; imageIndex++)
                {
                    string highResPath = Path.Combine(highResFolder, $"image_{imageIndex}.png");
                    if (!File.Exists(highResPath))
                    {
                        Console.Error.WriteLine($"High‑resolution image not found: {highResPath}");
                        continue;
                    }

                    using (FileStream imageStream = File.OpenRead(highResPath))
                    {
                        images.Replace(imageIndex, imageStream);
                    }
                }
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPath}'.");
    }
}
