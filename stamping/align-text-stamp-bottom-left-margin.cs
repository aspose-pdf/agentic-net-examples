using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for TextStamp

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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp stamp = new TextStamp("Sample Stamp");

            // Align the stamp to the bottom‑left corner with a 10‑point margin
            stamp.HorizontalAlignment = HorizontalAlignment.Left;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;
            stamp.LeftMargin          = 10; // 10 points from the left edge
            stamp.BottomMargin        = 10; // 10 points from the bottom edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                stamp.Put(page);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}