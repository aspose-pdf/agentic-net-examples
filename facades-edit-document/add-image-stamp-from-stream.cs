using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the image into a memory stream – no file is written to disk.
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            // Create an ImageStamp directly from the stream using the Drawing namespace.
            ImageStamp imgStamp = new ImageStamp(imgStream)
            {
                Background = false,               // place stamp on top of page content.
                Opacity = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Open the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // Apply the stamp to every page.
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
