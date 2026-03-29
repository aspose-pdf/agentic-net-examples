using System;
using System.IO;
using Aspose.Pdf;

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

        // Multiline stamp text (use \n for line breaks)
        string stampText = "Line 1\nLine 2\nLine 3";

        // Create a TextStamp with the multiline value
        TextStamp stamp = new TextStamp(stampText);
        // Right‑align the text within the stamp rectangle
        stamp.HorizontalAlignment = HorizontalAlignment.Right;
        // Position the stamp at the bottom of the page
        stamp.VerticalAlignment = VerticalAlignment.Bottom;
        // Optional bottom margin (float literal required)
        stamp.BottomMargin = 10f;

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            Page pageFour = doc.Pages[4];
            pageFour.AddStamp(stamp);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multiline right‑aligned text stamp added to page 4 and saved as '{outputPath}'.");
    }
}