using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagesFolder = "images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            Page page = doc.Pages[1];
            string[] imageFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string imagePath in imageFiles)
            {
                string extension = Path.GetExtension(imagePath).ToLowerInvariant();
                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp")
                {
                    continue;
                }

                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                try
                {
                    page.AddImage(imagePath, rect);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to add image '{imagePath}': {ex.Message}");
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
