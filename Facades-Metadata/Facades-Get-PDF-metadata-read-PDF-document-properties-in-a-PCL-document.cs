using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file whose metadata will be read
        string pdfPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath}");
            return;
        }

        // Load the PDF metadata using the PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

        // Output selected metadata properties to the console
        Console.WriteLine($"Title: {pdfInfo.Title}");
        Console.WriteLine($"Author: {pdfInfo.Author}");
        Console.WriteLine($"Subject: {pdfInfo.Subject}");
        Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
        Console.WriteLine($"Creator: {pdfInfo.Creator}");
        Console.WriteLine($"Producer: {pdfInfo.Producer}");
        Console.WriteLine($"Creation Date: {pdfInfo.CreationDate}");
        Console.WriteLine($"Modification Date: {pdfInfo.ModDate}");
        Console.WriteLine($"Number of Pages: {pdfInfo.NumberOfPages}");
        Console.WriteLine($"Is Encrypted: {pdfInfo.IsEncrypted}");
        Console.WriteLine($"Has Open Password: {pdfInfo.HasOpenPassword}");
        Console.WriteLine($"Has Edit Password: {pdfInfo.HasEditPassword}");
    }
}