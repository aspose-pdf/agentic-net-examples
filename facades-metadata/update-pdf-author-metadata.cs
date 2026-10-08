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

        try
        {
            // Load PDF file information using PdfFileInfo (Facades API)
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Set the new Author value
                pdfInfo.Author = newAuthor;

                // Persist the updated metadata to a new file
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Author updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}