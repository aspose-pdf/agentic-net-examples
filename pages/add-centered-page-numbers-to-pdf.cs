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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a page number stamp; default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();
                stamp.StartingNumber = 1;                     // start numbering at 1
                stamp.HorizontalAlignment = HorizontalAlignment.Center; // center horizontally

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}