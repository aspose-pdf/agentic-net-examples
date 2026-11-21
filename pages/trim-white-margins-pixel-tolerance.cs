using System;
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

        // Custom pixel tolerance per page (in points; 1 point = 1/72 inch)
        // Adjust this value as needed for each page.
        double[] tolerances = { 5, 10, 8 }; // example tolerances for first three pages

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Ensure we iterate using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Choose tolerance for the current page; if not enough values, use the last one.
                    double tolerance = (i <= tolerances.Length) ? tolerances[i - 1] : tolerances[tolerances.Length - 1];

                    // Get the current MediaBox (full page size)
                    Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                    // Create a new rectangle reduced by the tolerance on all sides.
                    // This effectively trims white space around the page content.
                    Aspose.Pdf.Rectangle trimmedBox = new Aspose.Pdf.Rectangle(
                        mediaBox.LLX + tolerance,
                        mediaBox.LLY + tolerance,
                        mediaBox.URX - tolerance,
                        mediaBox.URY - tolerance);

                    // Apply the new rectangle to both MediaBox and CropBox.
                    page.MediaBox = trimmedBox;
                    page.CropBox  = trimmedBox;
                }

                // Save the modified PDF.
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