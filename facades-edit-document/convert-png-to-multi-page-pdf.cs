using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PNG files – adjust the paths as needed
        string[] pngFiles = new string[]
        {
            "image1.png",
            "image2.png",
            "image3.png"
        };

        // Validate that all input files exist
        foreach (string file in pngFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // List to hold individual PDF streams (one per image)
        List<MemoryStream> pdfPages = new List<MemoryStream>();

        // Convert each PNG to a single‑page PDF stored in memory
        foreach (string pngPath in pngFiles)
        {
            using (Document imgDoc = new Document())
            {
                // Add a new page (default size) and place the image on it
                Page page = imgDoc.Pages.Add();

                Aspose.Pdf.Image img = new Aspose.Pdf.Image
                {
                    File = pngPath
                };
                page.Paragraphs.Add(img);

                // Save the one‑page PDF to a memory stream
                MemoryStream ms = new MemoryStream();
                imgDoc.Save(ms);
                ms.Position = 0; // Reset stream position for later reading
                pdfPages.Add(ms);
            }
        }

        // Output PDF file that will contain all pages
        const string outputPdf = "combined.pdf";

        // Use PdfFileEditor (Facade) to concatenate the in‑memory PDFs
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate accepts an array of streams and an output stream
        using (FileStream outStream = new FileStream(outputPdf, FileMode.Create, FileAccess.Write))
        {
            editor.Concatenate(pdfPages.ToArray(), outStream);
        }

        // Dispose all temporary memory streams
        foreach (MemoryStream ms in pdfPages)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Successfully created multi‑page PDF: {outputPdf}");
    }
}