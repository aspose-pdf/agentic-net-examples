using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // for TextStamp, FontRepository, etc.
using System.Drawing;          // for System.Drawing.Color (used by TextStamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, insert a blank page at position 3, then add a header to all pages.
        using (Document doc = new Document(inputPath))
        {
            // Insert a new blank page at position 3 (1‑based indexing).
            // The inserted page will adopt the most common page size in the document.
            doc.Pages.Insert(3);

            // Prepare a header using TextStamp (recommended over PdfFileStamp for headers).
            foreach (Page page in doc.Pages)
            {
                TextStamp headerStamp = new TextStamp("Document Header");
                headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                headerStamp.TextState.FontSize = 12;
                headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
                headerStamp.VerticalAlignment = VerticalAlignment.Top;
                headerStamp.YIndent = 20f; // top margin in points

                page.AddStamp(headerStamp);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
