using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputCsv = "images.csv";
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
            csvWriter.WriteLine("ImageFile,PageNumber,Width,Height");

            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                int imageIndex = 1;
                foreach (XImage img in page.Resources.Images)
                {
                    string imageFileName = $"page{pageIndex}_img{imageIndex}.png";
                    string imagePath = Path.Combine(imagesFolder, imageFileName);

                    // Save the image using a FileStream (XImage.Save expects a Stream)
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    csvWriter.WriteLine($"{imageFileName},{pageIndex},{img.Width},{img.Height}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Images extracted to '{imagesFolder}' and CSV saved to '{outputCsv}'.");
    }
}
