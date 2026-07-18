using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newAuthor = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file info, modify the Author, and save the changes.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Author = newAuthor;                     // Set new Author value
            bool saved = pdfInfo.SaveNewInfo(outputPath);   // Persist changes

            Console.WriteLine(saved
                ? $"Author updated and saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}