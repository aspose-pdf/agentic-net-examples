using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchAnnotationAuthorUpdater
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\PdfInput";
        // Folder where updated PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Author names: source (to replace) and destination (new value)
        const string sourceAuthor = "Old Author";
        const string newAuthor    = "New Author";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file path (adds "_updated" suffix)
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf");

            // Use PdfAnnotationEditor to modify annotation authors
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document into the editor
                editor.BindPdf(inputPath);

                // Determine the total number of pages (1‑based indexing)
                int pageCount = editor.Document.Pages.Count;

                // Update the author field for all annotations in the document
                editor.ModifyAnnotationsAuthor(1, pageCount, sourceAuthor, newAuthor);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
        }

        Console.WriteLine("Batch update completed.");
    }
}