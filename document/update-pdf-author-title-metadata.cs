using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newAuthor = "John Doe";
        const string newTitle = "Updated PDF Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Read existing metadata
            string currentAuthor = pdfDoc.Info.Author;
            string currentTitle = pdfDoc.Info.Title;
            Console.WriteLine($"Current Author: {currentAuthor}");
            Console.WriteLine($"Current Title : {currentTitle}");

            // Modify metadata
            pdfDoc.Info.Author = newAuthor;
            pdfDoc.Info.Title = newTitle;

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}