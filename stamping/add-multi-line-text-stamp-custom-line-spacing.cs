using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextState is defined here

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Multi‑line disclaimer text (use newline characters for separate lines)
            string disclaimer = "Disclaimer:\nThis document is confidential.\nDo not distribute.";

            // Create a TextStamp with the disclaimer string
            TextStamp stamp = new TextStamp(disclaimer);

            // Set custom line spacing (points) via the TextState of the stamp
            stamp.TextState.LineSpacing = 5f; // increase spacing between lines

            // Optional appearance settings
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;
            stamp.BottomMargin        = 20;   // distance from the bottom edge
            stamp.Opacity             = 0.7f; // semi‑transparent
            stamp.Background          = false; // draw on top of page content

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (extension .pdf ensures PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}