using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block
        using (Document pdfDocument = new Document(inputPath))
        {
            // Access and modify metadata via PdfFileInfo inside its own using block
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfDocument))
            {
                Console.WriteLine($"Original Title : {pdfInfo.Title}");
                Console.WriteLine($"Original Author: {pdfInfo.Author}");

                // Update metadata
                pdfInfo.Title = "Updated Title";
                pdfInfo.Author = "Updated Author";

                // Save the updated PDF to a new file
                pdfInfo.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}