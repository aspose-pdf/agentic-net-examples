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

        // Load PDF metadata using the Facades API
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update Author only if it is empty or null
            if (string.IsNullOrEmpty(pdfInfo.Author))
            {
                pdfInfo.Author = newAuthor;
                // Save the updated metadata to a new file
                bool success = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(success
                    ? $"Author set and saved to '{outputPath}'."
                    : "Failed to save updated PDF.");
            }
            else
            {
                Console.WriteLine($"Author already set to '{pdfInfo.Author}'. No changes made.");
            }
        }
    }
}