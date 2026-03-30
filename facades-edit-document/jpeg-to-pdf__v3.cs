using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string[] imagePaths = new string[] { "photo1.jpg", "photo2.jpg", "photo3.jpg" };
        const string outputPath = "combined.pdf";

        using (Document doc = new Document())
        {
            foreach (string imagePath in imagePaths)
            {
                if (!File.Exists(imagePath))
                {
                    Console.Error.WriteLine($"Image not found: {imagePath}");
                    continue;
                }

                Page page = doc.Pages.Add();
                Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                img.File = imagePath;
                page.Paragraphs.Add(img);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}