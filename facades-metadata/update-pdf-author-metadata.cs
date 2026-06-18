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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF file information
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update the Author metadata
            pdfInfo.Author = newAuthor;

            // Save the updated PDF to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Author updated successfully. Saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}