using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf"; // you can overwrite inputPath if desired

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Update metadata fields
        pdfInfo.Title   = "Updated Document Title";
        pdfInfo.Author  = "Jane Doe";
        pdfInfo.Subject = "Demonstration of SaveNewInfo";
        pdfInfo.Keywords = "Aspose.Pdf, Metadata, C#";

        // Persist the changes back to a PDF file (outputPath can be the same as inputPath to overwrite)
        pdfInfo.SaveNewInfo(outputPath);

        Console.WriteLine("PDF metadata has been updated successfully.");
    }
}
