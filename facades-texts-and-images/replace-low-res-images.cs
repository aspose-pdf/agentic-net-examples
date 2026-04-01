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

        try
        {
            using (Document document = new Document(inputPath))
            {
                foreach (Page page in document.Pages)
                {
                    Aspose.Pdf.XImageCollection images = page.Resources.Images;
                    for (int i = 1; i <= images.Count; i++)
                    {
                        string highResPath = Path.Combine(highResFolder, $"image{i}.png");
                        if (!File.Exists(highResPath))
                        {
                            Console.Error.WriteLine($"High‑resolution image not found: {highResPath}");
                            continue;
                        }

                        using (FileStream imageStream = File.OpenRead(highResPath))
                        {
                            images.Replace(i, imageStream);
                        }
                    }
                }

                document.Save(outputPath);
            }

            Console.WriteLine($"Images replaced and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
