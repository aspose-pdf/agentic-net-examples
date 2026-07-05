using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "input_pdfs";
        // Folder where the updated PDFs will be saved
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Use the PdfBookmarkEditor facade to modify bookmarks
                using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
                {
                    // Load the PDF file
                    editor.BindPdf(inputPath);

                    // Aspose.Pdf uses 1‑based page indexing
                    int lastPageNumber = editor.Document.Pages.Count;

                    // Add a bookmark titled "Reviewed" that points to the last page
                    editor.CreateBookmarkOfPage("Reviewed", lastPageNumber);

                    // Save the modified PDF (overwrites or creates a new file)
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}