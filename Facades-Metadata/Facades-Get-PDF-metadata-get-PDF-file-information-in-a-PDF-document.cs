using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect the PDF file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <pdfPath>");
            return;
        }

        string pdfPath = args[0];

        // Verify that the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Initialize PdfFileInfo with the PDF file path
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Output selected metadata properties
            Console.WriteLine($"Title: {pdfInfo.Title}");
            Console.WriteLine($"Author: {pdfInfo.Author}");
            Console.WriteLine($"Subject: {pdfInfo.Subject}");
            Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            Console.WriteLine($"Creator: {pdfInfo.Creator}");
            Console.WriteLine($"Producer: {pdfInfo.Producer}");
            Console.WriteLine($"CreationDate: {pdfInfo.CreationDate}");
            Console.WriteLine($"ModDate: {pdfInfo.ModDate}");
            Console.WriteLine($"Number of pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"IsEncrypted: {pdfInfo.IsEncrypted}");
            Console.WriteLine($"HasOpenPassword: {pdfInfo.HasOpenPassword}");
            Console.WriteLine($"HasEditPassword: {pdfInfo.HasEditPassword}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading PDF metadata: {ex.Message}");
        }
    }
}