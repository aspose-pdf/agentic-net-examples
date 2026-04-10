using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes (Document, Page, etc.)
using Aspose.Pdf.Facades;            // Facade classes (PdfBookmarkEditor)

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs to process.
        // Output folder where PDFs with the new bookmark will be saved.
        string inputFolder  = args.Length > 0 ? args[0] : "InputPdfs";
        string outputFolder = args.Length > 1 ? args[1] : "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder.
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
                using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
                {
                    // Bind the existing PDF file.
                    bookmarkEditor.BindPdf(pdfPath);

                    // Retrieve the total number of pages from the underlying Document.
                    int lastPageNumber = bookmarkEditor.Document.Pages.Count; // 1‑based indexing

                    // Add a bookmark titled "Reviewed" that points to the last page.
                    bookmarkEditor.CreateBookmarkOfPage("Reviewed", lastPageNumber);

                    // Save the modified PDF to the output location.
                    bookmarkEditor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch bookmark addition completed.");
    }
}