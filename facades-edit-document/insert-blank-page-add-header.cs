using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and insert a blank page at position 3 (1‑based indexing)
        using (Document doc = new Document(inputPath))
        {
            doc.Pages.Insert(3); // Inserts an empty page

            // Use PdfFileStamp facade to add a header to all pages
            using (PdfFileStamp stamp = new PdfFileStamp(doc))
            {
                // Add a simple text header with a top margin of 50 points
                stamp.AddHeader(new FormattedText("Header Text"), 50);

                // Save the modified PDF
                stamp.Save(outputPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}