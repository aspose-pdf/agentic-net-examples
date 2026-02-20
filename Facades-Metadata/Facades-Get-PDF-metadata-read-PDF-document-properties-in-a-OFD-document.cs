using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF operations

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (adjust as needed)
        const string pdfPath = "input.pdf";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Initialize PdfFileInfo with the PDF file.
            // This loads the document metadata without opening the full Document object.
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Output basic metadata properties
            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($" Title          : {pdfInfo.Title}");
            Console.WriteLine($" Author         : {pdfInfo.Author}");
            Console.WriteLine($" Subject        : {pdfInfo.Subject}");
            Console.WriteLine($" Keywords       : {pdfInfo.Keywords}");
            Console.WriteLine($" Creator        : {pdfInfo.Creator}");
            Console.WriteLine($" Producer       : {pdfInfo.Producer}");
            Console.WriteLine($" Creation Date  : {pdfInfo.CreationDate}");
            Console.WriteLine($" Modification   : {pdfInfo.ModDate}");
            Console.WriteLine($" Number of Pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($" Is Encrypted   : {pdfInfo.IsEncrypted}");
            Console.WriteLine($" Has Open Password: {pdfInfo.HasOpenPassword}");
            Console.WriteLine($" Has Edit Password: {pdfInfo.HasEditPassword}");
            Console.WriteLine($" Is PDF File    : {pdfInfo.IsPdfFile}");
            Console.WriteLine($" Has Collection : {pdfInfo.HasCollection}");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., invalid PDF, permission issues)
            Console.Error.WriteLine($"An error occurred while reading PDF metadata: {ex.Message}");
        }
    }
}