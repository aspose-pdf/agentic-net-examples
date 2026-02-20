using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Directory where individual pages will be saved
        const string outputDir = "output_pages";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF using the PdfFileInfo facade (facade‑based loading)
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);               // Load the document
            Document sourceDoc = pdfInfo.Document;    // Access the underlying Document

            int pageCount = sourceDoc.Pages.Count;    // Total number of pages

            // Iterate through each page and create a separate PDF file
            for (int i = 1; i <= pageCount; i++)
            {
                // Create a new empty document for the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the i‑th page from the source document
                    singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                    // Build the output file name (e.g., page_1.pdf)
                    string outPath = Path.Combine(outputDir, $"page_{i}.pdf");

                    // Save the single‑page document (using the provided save rule)
                    singlePageDoc.Save(outPath);

                    Console.WriteLine($"Page {i} saved to: {outPath}");
                }
            }
        }
    }
}