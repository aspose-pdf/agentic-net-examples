using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor has no BindPdf).
        Document pdfDoc = new Document(inputPath);

        // Verify that page 2 exists.
        if (pdfDoc.Pages.Count < 2)
        {
            Console.Error.WriteLine("The PDF does not contain a second page.");
            return;
        }

        // Rotate page 2 by 90 degrees clockwise using the Page.Rotate property and Rotation enum.
        pdfDoc.Pages[2].Rotate = Rotation.on90;

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Page 2 rotated and saved to '{outputPath}'.");
    }
}
