using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Drawing;            // Image class lives here

class Program
{
    static void Main()
    {
        // Input folder containing PNG images (adjust as needed)
        const string imagesFolder = "Images";
        // Output PDF file path
        const string outputPdf = "output.pdf";

        // Validate input folder
        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Get PNG files sorted alphabetically (default order)
        string[] pngFiles = Directory.GetFiles(imagesFolder, "*.png");
        Array.Sort(pngFiles, StringComparer.InvariantCulture);

        if (pngFiles.Length == 0)
        {
            Console.Error.WriteLine("No PNG files found in the specified folder.");
            return;
        }

        // Create a new PDF document (empty) and ensure proper disposal
        using (Document pdfDoc = new Document())
        {
            // Add each PNG as a separate page
            foreach (string pngPath in pngFiles)
            {
                // Create a new page (default size & margins)
                Page page = pdfDoc.Pages.Add();

                // Add the image to the page
                Image img = new Image
                {
                    File = pngPath
                };
                page.Paragraphs.Add(img);
            }

            // Save the multi‑page PDF with default margins
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}
