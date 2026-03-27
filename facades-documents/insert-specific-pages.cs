using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs (must be in the working directory)
        string destinationPath = "destination.pdf"; // PDF into which pages will be inserted
        string sourcePath = "source.pdf";           // PDF providing the pages to insert
        string outputPath = "merged.pdf";           // Resulting PDF file

        // Position in the destination PDF where pages will be inserted (1‑based index)
        int insertPosition = 2; // Insert after the first page

        // Specific pages from the source PDF to insert (1‑based page numbers)
        int[] pagesToInsert = new int[] { 3, 5, 7 };

        // Validate input files
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Perform the insertion using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Insert(destinationPath, insertPosition, sourcePath, pagesToInsert, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}