using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // required by the task specification

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the stamp once and reuse it for the selected pages
            ImageStamp stamp = new ImageStamp(stampPath)
            {
                // Example positioning – adjust as needed
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Opacity             = 0.5,
                Background          = false
            };

            // Pages are 1‑based in Aspose.Pdf
            int[] targetPages = { 1, 5, 10 };

            foreach (int pageNumber in targetPages)
            {
                if (pageNumber <= doc.Pages.Count)
                {
                    // AddStamp is a method on the Page object, not on the collection
                    doc.Pages[pageNumber].AddStamp(stamp);
                }
                else
                {
                    Console.WriteLine($"Page {pageNumber} does not exist in the document (total pages: {doc.Pages.Count}).");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied to pages 1,5,10. Output saved to '{outputPath}'.");
    }
}