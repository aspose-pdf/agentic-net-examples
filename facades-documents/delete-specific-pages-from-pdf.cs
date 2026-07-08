using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing as per Aspose.Pdf)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open streams with proper disposal
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Create the PdfFileEditor facade (does NOT implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and write the result to the output stream
            bool success = editor.Delete(inputStream, pagesToDelete, outputStream);

            if (success)
                Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} deleted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages.");
        }
    }
}