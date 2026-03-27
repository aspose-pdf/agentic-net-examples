using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing input PDF files
        const string inputFolder = "input_pdfs";
        // Author to replace and the new author name
        const string sourceAuthor = "Old Author";
        const string destinationAuthor = "New Author";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            // Bind the PDF to the editor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);

                // Determine the total number of pages in the document
                int pageCount = editor.Document.Pages.Count;

                // Modify the author of annotations on all pages
                editor.ModifyAnnotationsAuthor(1, pageCount, sourceAuthor, destinationAuthor);

                // Save the updated PDF with a simple filename (no directory path)
                string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf";
                editor.Save(outputFileName);
            }

            Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' -> updated file saved.");
        }
    }
}