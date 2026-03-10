using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Text;          // For TextStamp and related enums

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with desired text
            TextStamp stamp = new TextStamp("Confidential");

            // Set the alignment of the text inside the stamp
            // HorizontalAlignment.Center aligns the text to the center of the stamp rectangle
            stamp.TextAlignment = HorizontalAlignment.Center;

            // Optionally adjust other visual properties
            stamp.HorizontalAlignment = HorizontalAlignment.Center; // aligns the stamp on the page
            stamp.VerticalAlignment   = VerticalAlignment.Center;   // aligns the stamp vertically

            // Apply the stamp to each page in the document
            for (int i = 1; i <= doc.Pages.Count; i++) // page indexing is 1‑based
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with aligned TextStamp to '{outputPath}'.");
    }
}