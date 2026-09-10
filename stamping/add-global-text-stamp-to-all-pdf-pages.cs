using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextStamp

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be applied to every page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Position the stamp in the center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the stamp semi‑transparent and place it behind page content
                Opacity   = 0.5f,
                Background = true
            };

            // Apply the same stamp to all pages
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}