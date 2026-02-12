using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be supplied via command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
            {
                Page page = pdfDocument.Pages[pageNum];

                // Iterate over each image on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a simple alternate text; customize as needed
                    string altText = $"Image {img.Name ?? "Unnamed"} on page {pageNum}";

                    // Assign the alternate text for accessibility
                    img.TrySetAlternativeText(altText, page);
                }
            }

            // Save the modified PDF (uses the document-save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved with alternate text: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}