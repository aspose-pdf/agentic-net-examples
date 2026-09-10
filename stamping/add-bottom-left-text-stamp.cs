using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for HorizontalAlignment & VerticalAlignment enums

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp stamp = new TextStamp("Bottom‑Left Stamp");

            // Align to bottom‑left with a 10‑point margin
            stamp.BottomMargin = 10;               // 10 points from the bottom edge
            stamp.LeftMargin   = 10;               // 10 points from the left edge
            stamp.HorizontalAlignment = HorizontalAlignment.Left;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Apply the stamp to every page in the document
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}