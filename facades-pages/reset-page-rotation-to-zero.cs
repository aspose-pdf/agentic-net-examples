using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_reset_rotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Ensure the document has at least six pages.
        if (pdfDocument.Pages.Count < 6)
        {
            Console.Error.WriteLine("The PDF does not contain a page 6 to reset rotation.");
            return;
        }

        // Reset rotation of page 6 to zero degrees.
        // Use Rotation.None to represent 0° rotation.
        pdfDocument.Pages[6].Rotate = Rotation.None;

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Page 6 rotation reset. Saved to '{outputPath}'.");
    }
}
