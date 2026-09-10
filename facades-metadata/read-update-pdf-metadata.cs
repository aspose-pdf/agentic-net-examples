using System;
using System.IO;
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

        // Create a PdfFileInfo instance and bind the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);

            // Read existing metadata
            Console.WriteLine($"Title   : {pdfInfo.Title}");
            Console.WriteLine($"Author  : {pdfInfo.Author}");
            Console.WriteLine($"Subject : {pdfInfo.Subject}");
            Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            Console.WriteLine($"Creator : {pdfInfo.Creator}");
            Console.WriteLine($"Producer: {pdfInfo.Producer}");
            Console.WriteLine($"Pages   : {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Version : {pdfInfo.GetPdfVersion()}");

            // Modify metadata
            pdfInfo.Title = "New Document Title";
            pdfInfo.Author = "Jane Doe";
            pdfInfo.Subject = "Metadata manipulation example";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata";

            // Save the updated metadata to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}