using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\PdfInput";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Author names: source (to replace) and destination (new)
        const string sourceAuthor = "Old Author";
        const string newAuthor    = "New Author";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string inputPath in pdfFiles)
        {
            // Determine output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Bind the PDF to the annotation editor
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);

                    // Load the document to obtain page count
                    using (Document doc = new Document(inputPath))
                    {
                        int startPage = 1;                     // 1‑based indexing
                        int endPage   = doc.Pages.Count;       // last page
                        // Modify the author of all annotations in the page range
                        editor.ModifyAnnotationsAuthor(startPage, endPage, sourceAuthor, newAuthor);
                    }

                    // Save the modified PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}