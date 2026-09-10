using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed.pdf";
        const string outputRootFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRootFolder);

        // Load the signed PDF (Document implements IDisposable)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];

                // Create a folder for this page's images
                string pageFolder = Path.Combine(outputRootFolder, $"Page_{pageNumber}");
                Directory.CreateDirectory(pageFolder);

                int imageIndex = 1;

                // Iterate over the XImage collection (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Save each image as PNG using a FileStream (XImage.Save expects a Stream)
                    string imagePath = Path.Combine(pageFolder, $"Image_{imageIndex}.png");
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("All images have been extracted to page‑specific folders.");
    }
}
