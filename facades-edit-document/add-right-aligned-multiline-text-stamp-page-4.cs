using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Use PdfFileStamp facade to bind the source PDF and later save the result.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the PDF document.
            fileStamp.BindPdf(inputPath);

            // Create a multiline text stamp.
            // Newline characters create separate lines.
            TextStamp textStamp = new TextStamp("First line\nSecond line\nThird line");

            // Align the stamp to the right side of the page.
            textStamp.HorizontalAlignment = HorizontalAlignment.Right;

            // Place the stamp at the bottom of the page.
            textStamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Optional: set a bottom margin to keep the stamp away from the page edge.
            textStamp.BottomMargin = 20; // points

            // Add the stamp only to page 4 (pages are 1‑based).
            fileStamp.Document.Pages[4].AddStamp(textStamp);

            // Save the modified PDF.
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Text stamp added to page 4 and saved as '{outputPath}'.");
    }
}