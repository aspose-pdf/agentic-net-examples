using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class SplitPdfExample
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory where split pages will be saved
        const string outputDirectory = "SplitPages";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the source PDF document
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Get total number of pages (Aspose.Pdf collections are 1‑based)
                int totalPages = sourceDoc.Pages.Count;

                // Iterate through each page and save it as a separate PDF
                for (int pageIndex = 1; pageIndex <= totalPages; pageIndex++)
                {
                    // Create a new empty PDF document
                    using (Document singlePageDoc = new Document())
                    {
                        // Remove the default blank page added by the constructor
                        singlePageDoc.Pages.Delete(1);

                        // Add the current page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageIndex]);

                        // Build output file name
                        string outputPath = Path.Combine(outputDirectory, $"page_{pageIndex}.pdf");

                        // Save the split page (uses the provided document-save rule)
                        singlePageDoc.Save(outputPath);

                        Console.WriteLine($"Saved page {pageIndex} to '{outputPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while splitting the PDF: {ex.Message}");
        }
    }
}