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

        // Load the PDF document (using the recommended using pattern)
        using (Document doc = new Document(inputPath))
        {
            // Create a multiline text stamp. Newline characters create separate lines.
            TextStamp stamp = new TextStamp("First line of text\nSecond line of text\nThird line of text");

            // Align the stamp to the right side of the page.
            stamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;

            // Place the stamp at the bottom of the page.
            stamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Bottom;

            // Optional: set a bottom margin to keep the stamp away from the page edge.
            stamp.BottomMargin = 20;

            // Add the stamp only to page 4 (Aspose.Pdf uses 1‑based indexing).
            Page pageFour = doc.Pages[4];
            pageFour.AddStamp(stamp);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added to page 4 and saved as '{outputPath}'.");
    }
}