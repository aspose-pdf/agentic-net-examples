using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and bind it to a PdfFileInfo instance
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Read existing metadata
            Console.WriteLine($"Title:          {pdfInfo.Title}");
            Console.WriteLine($"Author:         {pdfInfo.Author}");
            Console.WriteLine($"Subject:        {pdfInfo.Subject}");
            Console.WriteLine($"Keywords:       {pdfInfo.Keywords}");
            Console.WriteLine($"CreationDate:   {pdfInfo.CreationDate}");
            Console.WriteLine($"ModDate:        {pdfInfo.ModDate}");
            Console.WriteLine($"Producer:       {pdfInfo.Producer}");
            Console.WriteLine($"Pages:          {pdfInfo.NumberOfPages}");
            Console.WriteLine($"PDF Version:    {pdfInfo.GetPdfVersion()}");

            // Modify metadata as needed
            pdfInfo.Title    = "Updated Document Title";
            pdfInfo.Author   = "Jane Doe";
            pdfInfo.Subject  = "Demonstration of PdfFileInfo";
            pdfInfo.Keywords = "Aspose.Pdf, metadata, example";

            // Persist the changes to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}