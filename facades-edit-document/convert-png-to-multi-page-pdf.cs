using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PNG files – adjust the paths as needed
        string[] pngFiles = {
            "image1.png",
            "image2.png",
            "image3.png"
        };

        // Output PDF file
        const string outputPdf = "merged.pdf";

        // List to hold individual PDF streams (one page per image)
        List<MemoryStream> pdfPages = new List<MemoryStream>();

        // Create a one‑page PDF for each PNG image
        foreach (string pngPath in pngFiles)
        {
            if (!File.Exists(pngPath))
            {
                Console.Error.WriteLine($"File not found: {pngPath}");
                continue;
            }

            // MemoryStream will hold the temporary PDF
            MemoryStream pageStream = new MemoryStream();

            // Use the core Document API inside a using block (lifecycle rule)
            using (Document doc = new Document())
            {
                // Add a new page (default size and margins)
                Page page = doc.Pages.Add();

                // Create an Image object and set its source file
                Aspose.Pdf.Image img = new Aspose.Pdf.Image
                {
                    File = pngPath
                };

                // Add the image to the page's paragraphs collection
                page.Paragraphs.Add(img);

                // Save the one‑page PDF into the memory stream
                doc.Save(pageStream);
            }

            // Reset stream position for later reading
            pageStream.Position = 0;
            pdfPages.Add(pageStream);
        }

        if (pdfPages.Count == 0)
        {
            Console.Error.WriteLine("No valid PNG images were processed.");
            return;
        }

        // Use PdfFileEditor (a Facades class) to concatenate the page PDFs
        PdfFileEditor editor = new PdfFileEditor();

        // Output stream for the final merged PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Concatenate all page streams into the output stream
            editor.Concatenate(pdfPages.ToArray(), outputStream);

            // Write the merged PDF to disk
            File.WriteAllBytes(outputPdf, outputStream.ToArray());
        }

        Console.WriteLine($"Merged PDF created at '{outputPdf}'.");
    }
}