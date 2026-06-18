using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where updated PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Author names: source (to replace) and destination (new)
        const string sourceAuthor = "Old Author";
        const string newAuthor    = "New Author";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            // Build output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Use PdfAnnotationEditor (facade) inside a using block for deterministic disposal
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(inputPath);

                    // Determine the total number of pages in the document
                    int pageCount = editor.Document.Pages.Count; // 1‑based indexing

                    // Modify the author of all annotations on all pages
                    editor.ModifyAnnotationsAuthor(1, pageCount, sourceAuthor, newAuthor);

                    // Save the modified PDF to the output location
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation author update completed.");
    }
}