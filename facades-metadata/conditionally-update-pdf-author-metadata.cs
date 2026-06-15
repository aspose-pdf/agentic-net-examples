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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata using the Facade class.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update only when the Author field is empty or whitespace.
            if (string.IsNullOrWhiteSpace(pdfInfo.Author))
            {
                pdfInfo.Author = newAuthor;

                // Save the updated metadata to a new file.
                bool success = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(success
                    ? $"Author set and saved to '{outputPath}'."
                    : "Failed to save the updated PDF.");
            }
            else
            {
                Console.WriteLine($"Existing Author: '{pdfInfo.Author}'. No changes applied.");
            }
        }
    }
}