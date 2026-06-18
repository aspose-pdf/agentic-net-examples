using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create a page number stamp; default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Center the stamp horizontally on the page
                stamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

                // Position the stamp at the bottom of the page (can adjust margin if needed)
                stamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Bottom;

                // Ensure numbering starts at 1 (default, set explicitly for clarity)
                stamp.StartingNumber = 1;

                // Add the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}