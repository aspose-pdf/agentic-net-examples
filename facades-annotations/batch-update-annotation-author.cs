using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchAnnotationAuthorUpdater
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where updated PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Author names – replace as needed
        const string sourceAuthor = "Old Author";
        const string destinationAuthor = "New Author";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Prepare output path with same file name
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Initialize the annotation editor facade
                PdfAnnotationEditor editor = new PdfAnnotationEditor();

                // Bind the PDF document
                editor.BindPdf(inputPath);

                // Determine the page range (Aspose.Pdf uses 1‑based indexing)
                int startPage = 1;
                int endPage = editor.Document.Pages.Count;

                // Modify the author of all matching annotations in the range
                editor.ModifyAnnotationsAuthor(startPage, endPage, sourceAuthor, destinationAuthor);

                // Save the modified PDF
                editor.Save(outputPath);

                // Close the facade (releases the bound document)
                editor.Close();

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch update completed.");
    }
}