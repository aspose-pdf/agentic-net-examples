using System;
using System.IO;
using Aspose.Pdf;                 // Core API for PDF creation
using Aspose.Pdf.Facades;        // Facades namespace as required

class Program
{
    static void Main()
    {
        // Input folder containing PNG images (adjust as needed)
        const string imagesFolder = "Images";
        // Output PDF file path
        const string outputPdf = "Combined.pdf";

        // Validate input folder
        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Get PNG files sorted alphabetically (default order)
        string[] pngFiles = Directory.GetFiles(imagesFolder, "*.png");
        Array.Sort(pngFiles, StringComparer.OrdinalIgnoreCase);

        if (pngFiles.Length == 0)
        {
            Console.Error.WriteLine("No PNG files found in the specified folder.");
            return;
        }

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document())
        {
            // Add a page for each PNG image
            foreach (string pngPath in pngFiles)
            {
                // Add a new blank page (default size, default margins)
                Page page = pdfDoc.Pages.Add();

                // Create an Image object and set its source file
                // Fully qualify to avoid ambiguity with System.Drawing.Image
                Aspose.Pdf.Image img = new Aspose.Pdf.Image
                {
                    File = pngPath
                };

                // Add the image to the page's content
                page.Paragraphs.Add(img);
            }

            // Save the assembled multi‑page PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}