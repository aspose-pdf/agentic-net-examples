using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_alt.pdf";
        const string altText = "Descriptive alternate text for image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages (1‑based indexing)
                foreach (Page page in doc.Pages)
                {
                    // Iterate over each XImage resource on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Set alternative text for the image on this page
                        // Returns true if the operation succeeded
                        bool success = img.TrySetAlternativeText(altText, page);
                        Console.WriteLine($"Page {page.Number}: Image alt text set = {success}");
                    }
                }

                // Save the modified PDF (lifecycle: save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with updated image alternate text saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}