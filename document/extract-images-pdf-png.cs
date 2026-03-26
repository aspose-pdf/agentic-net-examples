using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int imageIndex = 1;
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                foreach (XImage image in page.Resources.Images)
                {
                    string outputFile = $"image_{imageIndex}_page{pageNumber}.png";
                    // XImage.Save expects a Stream; using FileStream preserves the original image data and resolution.
                    using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                    {
                        image.Save(fs);
                    }
                    Console.WriteLine($"Saved {outputFile}");
                    imageIndex++;
                }
            }
        }
    }
}
