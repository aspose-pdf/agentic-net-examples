using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF file
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load PDF metadata using the PdfFileInfo facade
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Output common metadata properties
            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($" Title          : {pdfInfo.Title}");
            Console.WriteLine($" Author         : {pdfInfo.Author}");
            Console.WriteLine($" Subject        : {pdfInfo.Subject}");
            Console.WriteLine($" Keywords       : {pdfInfo.Keywords}");
            Console.WriteLine($" Creator        : {pdfInfo.Creator}");
            Console.WriteLine($" Producer       : {pdfInfo.Producer}");
            Console.WriteLine($" Creation Date  : {pdfInfo.CreationDate}");
            Console.WriteLine($" Modification   : {pdfInfo.ModDate}");
            Console.WriteLine($" Pages          : {pdfInfo.NumberOfPages}");
            Console.WriteLine($" Encrypted      : {pdfInfo.IsEncrypted}");
            Console.WriteLine($" Has Open Pass : {pdfInfo.HasOpenPassword}");
            Console.WriteLine($" Has Edit Pass : {pdfInfo.HasEditPassword}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while reading PDF metadata: {ex.Message}");
        }
    }
}