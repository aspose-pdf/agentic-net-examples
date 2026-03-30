using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputCsv = "images_manifest.csv";
        const string imagesFolder = "extracted_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(imagesFolder);

        using (Document doc = new Document(inputPath))
        using (StreamWriter csvWriter = new StreamWriter(outputCsv, false))
        {
            csvWriter.WriteLine("FileName,PageNumber,Width,Height");

            int pageNumber = 1;
            foreach (Page page in doc.Pages)
            {
                int imageIndex = 1;
                foreach (XImage img in page.Resources.Images)
                {
                    string fileName = $"image_page{pageNumber}_img{imageIndex}.png";
                    string filePath = Path.Combine(imagesFolder, fileName);

                    // XImage.Save expects a Stream, so use a FileStream.
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    csvWriter.WriteLine($"{fileName},{pageNumber},{img.Width},{img.Height}");
                    imageIndex++;
                }
                pageNumber++;
            }
        }

        Console.WriteLine($"Image extraction complete. Manifest saved to {outputCsv}");
    }
}