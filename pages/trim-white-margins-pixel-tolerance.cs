using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define a custom pixel tolerance for each page (optional).
        // If a page number is not present in the dictionary, a default tolerance will be used.
        var pageTolerances = new Dictionary<int, double>
        {
            { 1, 15.0 }, // page 1: trim 15 pixels from each side
            { 2, 20.0 }  // page 2: trim 20 pixels from each side
            // add more entries as needed
        };
        const double defaultTolerance = 10.0; // used when a page has no specific entry

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Iterate using 1‑based page indexing (Aspose.Pdf requirement)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Retrieve the tolerance for the current page
                    double tolerance = pageTolerances.TryGetValue(i, out double t) ? t : defaultTolerance;

                    // Ensure tolerance does not exceed half of the page dimensions
                    double halfWidth  = page.PageInfo.Width  / 2.0;
                    double halfHeight = page.PageInfo.Height / 2.0;
                    if (tolerance > halfWidth)  tolerance = halfWidth;
                    if (tolerance > halfHeight) tolerance = halfHeight;

                    // Create a new rectangle that trims the specified amount from each side
                    Aspose.Pdf.Rectangle trimmedRect = new Aspose.Pdf.Rectangle(
                        tolerance,                                 // lower‑left X
                        tolerance,                                 // lower‑left Y
                        page.PageInfo.Width  - tolerance,          // upper‑right X
                        page.PageInfo.Height - tolerance);         // upper‑right Y

                    // Apply the rectangle as the page's CropBox (effective visible area)
                    page.CropBox = trimmedRect;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}