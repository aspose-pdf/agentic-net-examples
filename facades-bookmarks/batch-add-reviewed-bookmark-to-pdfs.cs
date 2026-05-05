using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\PdfBatch\Input";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_reviewed.pdf");

            try
            {
                // Initialize the bookmark editor facade
                PdfBookmarkEditor editor = new PdfBookmarkEditor();

                // Bind the PDF file to the facade
                editor.BindPdf(inputPath);

                // Retrieve the total number of pages from the underlying Document
                int lastPageNumber = editor.Document.Pages.Count;

                // Add a bookmark titled "Reviewed" that points to the last page
                editor.CreateBookmarkOfPage("Reviewed", lastPageNumber);

                // Save the modified PDF to the output location
                editor.Save(outputPath);

                // Release resources associated with the facade
                editor.Close();

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}