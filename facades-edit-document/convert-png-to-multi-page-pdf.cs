using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PNG files (adjust the paths as needed)
        string[] pngFiles = {
            "image1.png",
            "image2.png",
            "image3.png"
        };

        // Output PDF file
        const string outputPdf = "combined.pdf";

        // Validate input files
        foreach (string file in pngFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // List to hold individual PDF streams (one page per image)
        List<MemoryStream> pdfPages = new List<MemoryStream>();

        // Create a one‑page PDF for each PNG image
        foreach (string pngPath in pngFiles)
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a blank page
                Page page = doc.Pages.Add();

                // Add the PNG image to the page
                Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                img.File = pngPath;               // Set image source
                page.Paragraphs.Add(img);         // Place image on the page

                // Save the single‑page PDF to a memory stream
                MemoryStream ms = new MemoryStream();
                doc.Save(ms);
                ms.Position = 0;                  // Reset stream position for reading
                pdfPages.Add(ms);
            }
        }

        // Use PdfFileEditor (Aspose.Pdf.Facades) to concatenate the page PDFs
        PdfFileEditor editor = new PdfFileEditor();

        // Prepare output stream for the final PDF
        using (FileStream outStream = new FileStream(outputPdf, FileMode.Create, FileAccess.Write))
        {
            // Concatenate all page streams into one PDF
            editor.Concatenate(pdfPages.ToArray(), outStream);
        }

        // Dispose all intermediate memory streams
        foreach (MemoryStream ms in pdfPages)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Successfully created multi‑page PDF: {outputPdf}");
    }
}