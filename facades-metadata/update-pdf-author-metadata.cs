using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newAuthor  = "John Doe";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo implements IDisposable, so use a using block for deterministic cleanup
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Set the new Author metadata value
            pdfInfo.Author = newAuthor;

            // Persist the changes to a new file; SaveNewInfo returns true on success
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Author updated and saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}