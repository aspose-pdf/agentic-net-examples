using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for alignment enums
using Aspose.Pdf.Text;       // for TextState

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp stamp = new TextStamp("Bottom‑Left Stamp");

            // Set visual appearance (optional)
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            stamp.TextState.FontSize = 12;

            // Align to bottom‑left corner with 10‑point margins
            stamp.HorizontalAlignment = HorizontalAlignment.Left;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;
            stamp.LeftMargin          = 10; // points from the left edge
            stamp.BottomMargin        = 10; // points from the bottom edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                stamp.Put(page);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}