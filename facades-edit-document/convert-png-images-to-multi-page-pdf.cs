using System;
using System.IO;
using Aspose.Pdf;               // Core API for PDF creation
using Aspose.Pdf.Facades;      // Required by task specification

class Program
{
    static void Main()
    {
        // Folder containing the PNG images (adjust as needed)
        const string imagesFolder = "Images";
        // Output PDF file path
        const string outputPdf = "output.pdf";

        // Verify the images folder exists
        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Get all PNG files in the folder, sorted alphabetically
        string[] pngFiles = Directory.GetFiles(imagesFolder, "*.png");
        Array.Sort(pngFiles, StringComparer.OrdinalIgnoreCase);

        if (pngFiles.Length == 0)
        {
            Console.Error.WriteLine("No PNG images found to convert.");
            return;
        }

        // Create a new PDF document (empty) and ensure proper disposal
        using (Document pdfDoc = new Document())
        {
            // Add each PNG as a separate page
            foreach (string pngPath in pngFiles)
            {
                // Add a new page to the document (1‑based indexing)
                Page page = pdfDoc.Pages.Add();

                // Create an Image object and set its source file
                Image img = new Image
                {
                    File = pngPath
                };

                // Add the image to the page's paragraphs collection
                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF; extension is .pdf so no SaveOptions needed
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Successfully created multi‑page PDF: {outputPdf}");
    }
}