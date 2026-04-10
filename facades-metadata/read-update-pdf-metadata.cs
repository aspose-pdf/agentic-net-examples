using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfFileInfo facade and bind the PDF document
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);

            // Read existing metadata
            Console.WriteLine($"Title   : {pdfInfo.Title}");
            Console.WriteLine($"Author  : {pdfInfo.Author}");
            Console.WriteLine($"Subject : {pdfInfo.Subject}");
            Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            Console.WriteLine($"Pages   : {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Version : {pdfInfo.GetPdfVersion()}");

            // Modify metadata
            pdfInfo.Title    = "Updated Document Title";
            pdfInfo.Author   = "Jane Doe";
            pdfInfo.Subject  = "Aspose.Pdf Metadata Example";
            pdfInfo.Keywords = "Aspose.Pdf;Metadata;Example";

            // Save the updated metadata to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}