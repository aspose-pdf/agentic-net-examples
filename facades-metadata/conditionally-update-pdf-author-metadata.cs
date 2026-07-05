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

        // Load PDF metadata using the Facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update Author only when it is empty or whitespace
            if (string.IsNullOrWhiteSpace(pdfInfo.Author))
            {
                pdfInfo.Author = newAuthor;

                // Save the updated metadata to a new file
                bool success = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(success
                    ? $"Author set to \"{newAuthor}\" and saved as \"{outputPath}\"."
                    : "Failed to save updated PDF metadata.");
            }
            else
            {
                Console.WriteLine($"Existing Author: \"{pdfInfo.Author}\". No update performed.");
            }
        }
    }
}