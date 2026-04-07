using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextStamp alignment enums

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_faint_overlay.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify, and save – all within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be placed on every page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // 0.4 opacity makes the stamp faint
                Opacity = 0.4,

                // Position the stamp in the center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional: make the stamp appear behind page content
                Background = true
            };

            // Apply the same stamp to each page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Faint overlay added and saved to '{outputPath}'.");
    }
}