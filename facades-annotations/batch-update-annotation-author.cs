using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = @"C:\PdfFiles";
        // Folder where the updated PDFs will be saved
        const string outputFolder = @"C:\PdfFiles\Updated";
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

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_updated.pdf");

                // Load the document to obtain page and annotation counts
                Document doc = new Document(inputPath);
                int pageCount = doc.Pages.Count;

                // Use PdfAnnotationEditor to modify the author of every annotation
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the same PDF to the editor (the editor works on the file on disk)
                    editor.BindPdf(inputPath);

                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        // Annotations collection is 0‑based, but ModifyAnnotationsAuthor expects 1‑based index
                        int annotCount = doc.Pages[pageIndex].Annotations.Count;
                        for (int annotIndex = 1; annotIndex <= annotCount; annotIndex++)
                        {
                            // The overload expects: pageIndex, annotIndex, sourceAuthor, destinationAuthor
                            // Passing an empty string for sourceAuthor updates any existing author.
                            editor.ModifyAnnotationsAuthor(pageIndex, annotIndex, string.Empty, newAuthor);
                        }
                    }

                    // Save the modified PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation author update completed.");
    }
}
