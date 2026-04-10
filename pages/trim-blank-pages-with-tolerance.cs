using System;
using System.IO;
using Aspose.Pdf;

class TrimWhiteSpace
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

        // Custom pixel tolerance for each page (range 0..1). Adjust as needed.
        // Here we use the same tolerance for all pages; you can replace with an array per page.
        const double tolerance = 0.02; // 2 % fill threshold

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // Iterate pages in reverse order so that removal does not affect indexing.
                for (int i = doc.Pages.Count; i >= 1; i--)
                {
                    Page page = doc.Pages[i];

                    // Determine if the page is essentially blank (white space) using the tolerance.
                    // IsBlank returns true when the filled area ratio is less than the supplied factor.
                    if (page.IsBlank(tolerance))
                    {
                        // Remove the blank page.
                        doc.Pages.Delete(i);
                        Console.WriteLine($"Removed blank page {i} (tolerance {tolerance}).");
                    }
                    else
                    {
                        // Optionally, you could adjust the TrimBox here if you have logic to compute
                        // the content bounds. For demonstration we keep the existing TrimBox.
                        // Example:
                        // page.TrimBox = new Rectangle(page.MediaBox.LLX, page.MediaBox.LLY,
                        //                              page.MediaBox.URX, page.MediaBox.URY);
                    }
                }

                // Save the modified PDF (lifecycle rule: use Save inside using)
                doc.Save(outputPath);
                Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}