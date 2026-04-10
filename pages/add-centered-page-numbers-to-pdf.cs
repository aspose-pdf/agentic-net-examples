using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // not needed but harmless

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a page number stamp; default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Set the number that will appear on this page
                stamp.StartingNumber = i;               // sequential numbering starting at 1
                stamp.HorizontalAlignment = HorizontalAlignment.Center; // center horizontally
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;   // place at bottom (optional)

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}