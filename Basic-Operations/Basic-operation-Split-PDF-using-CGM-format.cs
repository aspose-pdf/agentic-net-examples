using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input CGM file path (replace with your actual file)
        const string cgmPath = "input.cgm";
        // Output directory where split PDFs will be saved
        const string outputDir = "SplitPages";

        // Validate input file
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the CGM file into a PDF document using default load options
            var loadOptions = new CgmLoadOptions(); // default A4 300dpi page size
            Document pdfDoc = new Document(cgmPath, loadOptions);

            // Iterate over each page (Aspose.Pdf collections are 1‑based)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Create a new document for the single page
                Document singlePageDoc = new Document();

                // Add the current page to the new document
                // The Add method clones the page into the target document
                singlePageDoc.Pages.Add(pdfDoc.Pages[i]);

                // Build output file name
                string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

                // Save the single‑page PDF
                // Uses the provided document-save rule
                singlePageDoc.Save(outPath);

                Console.WriteLine($"Saved page {i} to '{outPath}'.");
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}