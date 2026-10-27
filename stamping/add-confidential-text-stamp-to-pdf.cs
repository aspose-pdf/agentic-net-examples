using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "confidential_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired label
            TextStamp stamp = new TextStamp("Confidential")
            {
                // Set opacity to 0.6 for a subtle appearance
                Opacity = 0.6,

                // Optional: position the stamp (centered both horizontally and vertically)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional: make the stamp non‑background so it appears on top of content
                Background = false
            };

            // Add the stamp to each page individually (Page.AddStamp, not PageCollection)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}