using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDir = "InputPdfs";
        // Output directory for the modified PDFs
        const string outputDir = "OutputPdfs";

        // Author names: source (to replace) and destination (new value)
        const string srcAuthor = "Old Author";
        const string desAuthor = "New Author";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' does not exist. Please create it and place PDF files inside before running the program.");
            return;
        }

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDir}'. Nothing to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            // Determine output file path (same name, different folder)
            string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

            // Use PdfAnnotationEditor to modify annotation authors
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document into the editor
                editor.BindPdf(inputPath);

                // Determine the total number of pages (1‑based indexing)
                int pageCount = editor.Document.Pages.Count;

                // Update the author for all annotations on all pages
                editor.ModifyAnnotationsAuthor(1, pageCount, srcAuthor, desAuthor);

                // Save the modified PDF
                editor.Save(outputPath);

                // The using statement disposes the editor; explicit Close() is optional
                // editor.Close();
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }
    }
}