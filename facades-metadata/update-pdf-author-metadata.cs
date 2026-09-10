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

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF metadata using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update the Author property
            pdfInfo.Author = newAuthor;

            // Save the updated PDF to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Author updated successfully. Saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}