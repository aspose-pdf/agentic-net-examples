using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_disclaimer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with multi‑line disclaimer text
            TextStamp disclaimerStamp = new TextStamp("Disclaimer:\nAll information is confidential.\nDo not distribute.");

            // Configure appearance and layout
            disclaimerStamp.HorizontalAlignment = HorizontalAlignment.Center;
            disclaimerStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            // Use YIndent instead of the non‑existent Margin property to set distance from the bottom edge
            disclaimerStamp.YIndent = 20; // distance from bottom edge in points

            // Set font and custom line spacing (e.g., 1.5 times the default)
            disclaimerStamp.TextState.Font      = FontRepository.FindFont("Arial");
            disclaimerStamp.TextState.FontSize  = 10;
            disclaimerStamp.TextState.LineSpacing = 1.5f;   // custom line spacing

            // Apply the same stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(disclaimerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with disclaimer stamp to '{outputPath}'.");
    }
}
