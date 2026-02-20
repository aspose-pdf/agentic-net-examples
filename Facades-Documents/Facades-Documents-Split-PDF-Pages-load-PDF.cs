using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class SplitPdfPages
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output folder (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: SplitPdfPages <input-pdf> <output-folder>");
            return;
        }

        string inputPdfPath = args[0];
        string outputFolder = args[1];

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        try
        {
            // Use Facade to obtain basic information (demonstrates Aspose.Pdf.Facades usage)
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath);
            int pageCount = pdfInfo.NumberOfPages;
            Console.WriteLine($"PDF contains {pageCount} page(s).");

            // Load the document with the core API (required for page extraction)
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                for (int i = 1; i <= pageCount; i++) // Aspose.Pdf collections are 1‑based
                {
                    // Create a new empty document for the single page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page from the source document.
                        // The Add method copies the page into the new document.
                        singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Build output file name
                        string outputPath = Path.Combine(outputFolder, $"Page_{i}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outputPath);
                        Console.WriteLine($"Saved page {i} to '{outputPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}