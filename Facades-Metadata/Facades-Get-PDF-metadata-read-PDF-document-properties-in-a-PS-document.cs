using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file
        string pdfPath = "input.pdf";

        // Verify that the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Initialize PdfFileInfo with the PDF file path
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Retrieve and display metadata
            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($"Title          : {pdfInfo.Title}");
            Console.WriteLine($"Author         : {pdfInfo.Author}");
            Console.WriteLine($"Subject        : {pdfInfo.Subject}");
            Console.WriteLine($"Keywords       : {pdfInfo.Keywords}");
            Console.WriteLine($"Creator        : {pdfInfo.Creator}");
            Console.WriteLine($"Producer       : {pdfInfo.Producer}");
            Console.WriteLine($"Creation Date  : {pdfInfo.CreationDate}");
            Console.WriteLine($"Modification Date: {pdfInfo.ModDate}");
            Console.WriteLine($"Number of Pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Is Encrypted   : {pdfInfo.IsEncrypted}");
            Console.WriteLine($"Is PDF File    : {pdfInfo.IsPdfFile}");
            Console.WriteLine($"Has Open Password : {pdfInfo.HasOpenPassword}");
            Console.WriteLine($"Has Edit Password : {pdfInfo.HasEditPassword}");
            Console.WriteLine($"Header (custom): {pdfInfo.Header}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while reading PDF metadata: {ex.Message}");
        }
    }
}