using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = @"C:\PdfFiles\Input";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\PdfFiles\Output";
        // New author name to set for all annotations
        const string newAuthor = "John Doe";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_modified.pdf");

                // Determine the total number of pages using Document (PdfAnnotationEditor does not expose GetPageCount)
                int pageCount;
                using (Document tempDoc = new Document(inputPath))
                {
                    pageCount = tempDoc.Pages.Count;
                }

                // Bind the PDF, modify annotation authors, and save the result.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the source PDF
                    editor.BindPdf(inputPath);

                    // Modify the author of all annotations on every page.
                    // Parameters: startPage, endPage, sourceAuthor, destinationAuthor.
                    // An empty string for sourceAuthor matches annotations regardless of their current author.
                    editor.ModifyAnnotationsAuthor(1, pageCount, string.Empty, newAuthor);

                    // Save the modified PDF.
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}.pdf -> {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation author update completed.");
    }
}
