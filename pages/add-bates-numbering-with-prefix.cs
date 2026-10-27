using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";
        const string prefix = "PRJ-"; // alphanumeric prefix for tracking

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (see global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextStamp with the desired Bates number
                TextStamp stamp = new TextStamp($"{prefix}{i:D4}")
                {
                    // Position the stamp (adjust as needed)
                    XIndent = 20,          // distance from left edge
                    YIndent = 20,          // distance from bottom edge
                    // Ensure the stamp appears on top of page content
                    Background = false
                };

                // Configure appearance via the existing TextState instance
                stamp.TextState.Font = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize = 12;
                stamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied and saved to '{outputPath}'.");
    }
}
