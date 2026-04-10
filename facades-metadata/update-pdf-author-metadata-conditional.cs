using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileInfo resides here

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

        // Load the PDF file using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Check if the Author field is empty or null
            if (string.IsNullOrEmpty(pdfInfo.Author))
            {
                // Update the Author metadata
                pdfInfo.Author = newAuthor;

                // Save the updated PDF to a new file
                bool saved = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(saved
                    ? $"Metadata updated and saved to '{outputPath}'."
                    : "Failed to save updated PDF.");
            }
            else
            {
                Console.WriteLine("Author metadata already set; no changes made.");
            }
        }
    }
}