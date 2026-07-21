using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for HorizontalAlignment, VerticalAlignment enums

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a multiline text stamp
        // Newline characters create separate lines
        TextStamp stamp = new TextStamp("First line\nSecond line\nThird line")
        {
            // Align the stamp to the right side of the page
            HorizontalAlignment = HorizontalAlignment.Right,
            // Place the stamp at the bottom of the page
            VerticalAlignment   = VerticalAlignment.Bottom,
            // Optional margin from the bottom edge
            BottomMargin        = 20
        };

        // Add the stamp only to page 4 (Aspose.Pdf uses 1‑based indexing)
        Document doc = fileStamp.Document;
        if (doc.Pages.Count >= 4)
        {
            Page pageFour = doc.Pages[4];
            pageFour.AddStamp(stamp);
        }
        else
        {
            Console.Error.WriteLine("The document has fewer than 4 pages.");
        }

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamp added to page 4 and saved as '{outputPdf}'.");
    }
}