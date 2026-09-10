using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPdfProcessor
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Pages to delete from each file (1‑based indexing)
        // Example: delete pages 2 and 4 from every document
        int[] pagesToDelete = new int[] { 2, 4 };

        // Folder for intermediate cleaned PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "CleanedPdfs");
        Directory.CreateDirectory(tempFolder);

        // Array to hold paths of cleaned PDFs
        string[] cleanedFiles = new string[inputFiles.Length];

        // Delete specified pages from each input PDF
        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inputPath = inputFiles[i];
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            string cleanedPath = Path.Combine(tempFolder, $"cleaned_{i + 1}.pdf");
            PdfFileEditor editor = new PdfFileEditor();

            // Delete pages and write to a temporary file
            bool deleted = editor.Delete(inputPath, pagesToDelete, cleanedPath);
            if (!deleted)
            {
                Console.Error.WriteLine($"Failed to delete pages from: {inputPath}");
                return;
            }

            cleanedFiles[i] = cleanedPath;
        }

        // Concatenate all cleaned PDFs into a single document
        string outputPath = "merged_output.pdf";
        PdfFileEditor concatEditor = new PdfFileEditor();

        // Note: In evaluation mode Aspose.Pdf can handle up to 4 pages per document.
        // If any cleaned PDF exceeds this limit, the operation may throw an IndexOutOfRangeException.
        // A full license removes this restriction.
        bool concatenated = concatEditor.Concatenate(cleanedFiles, outputPath);
        if (!concatenated)
        {
            Console.Error.WriteLine("Failed to concatenate cleaned PDFs.");
            return;
        }

        Console.WriteLine($"Successfully created merged PDF: {outputPath}");
    }
}