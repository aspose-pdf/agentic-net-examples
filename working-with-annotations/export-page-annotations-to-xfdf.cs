using System;
using System.IO;
using Aspose.Pdf;

class ExportPageAnnotations
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Determine the page whose annotations we want to export (1‑based index)
        const int pageNumber = 1; // change as needed

        // Build the XFDF file name – same folder, same base name, .xfdf extension
        string xfdfPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf");

        try
        {
            // Load the original PDF
            using (Document sourceDoc = new Document(pdfPath))
            {
                // Create a temporary document that contains only the desired page
                using (Document singlePageDoc = new Document())
                {
                    // Add the selected page (pages are 1‑based)
                    singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                    // Export all annotations of this single‑page document to XFDF
                    singlePageDoc.ExportAnnotationsToXfdf(xfdfPath);
                }
            }

            Console.WriteLine($"Annotations from page {pageNumber} exported to: {xfdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}