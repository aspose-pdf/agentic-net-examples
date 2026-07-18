using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file whose metadata will be accessed.
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a PdfFileInfo instance and bind it to the PDF file.
        // The constructor that accepts a file path automatically loads the document.
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Example: read some metadata properties.
            Console.WriteLine($"Title   : {info.Title}");
            Console.WriteLine($"Author  : {info.Author}");
            Console.WriteLine($"Subject : {info.Subject}");
            Console.WriteLine($"Keywords: {info.Keywords}");
            Console.WriteLine($"Created : {info.CreationDate}");
            Console.WriteLine($"Modified: {info.ModDate}");
            Console.WriteLine($"Pages   : {info.NumberOfPages}");
            Console.WriteLine($"PDF Version: {info.GetPdfVersion()}");
            Console.WriteLine($"Is Encrypted: {info.IsEncrypted}");
        }
    }
}