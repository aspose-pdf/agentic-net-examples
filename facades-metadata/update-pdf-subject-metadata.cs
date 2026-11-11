using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_updated.pdf";
        const string newSubject = "Document purpose: Accessibility demonstration";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo works with the Facades API; it does not implement IDisposable.
        PdfFileInfo pdfInfo = new PdfFileInfo();

        // Load the existing PDF.
        pdfInfo.BindPdf(inputPath);

        // Update the Subject metadata field.
        pdfInfo.Subject = newSubject;

        // Save the modified PDF to a new file.
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Subject updated and saved to '{outputPath}'.");
    }
}