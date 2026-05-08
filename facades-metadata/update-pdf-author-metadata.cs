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

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF file information facade
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Update the Author metadata
        pdfInfo.Author = newAuthor;

        // Persist the changes to a new PDF file
        bool saved = pdfInfo.SaveNewInfo(outputPath);
        if (saved)
        {
            Console.WriteLine($"Author updated successfully. Saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to save the updated PDF.");
        }

        // Release resources held by the facade
        pdfInfo.Close();
    }
}