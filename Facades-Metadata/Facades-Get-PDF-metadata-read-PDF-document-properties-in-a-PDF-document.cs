using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file whose metadata will be read
        const string pdfPath = "sample.pdf";

        // Verify that the file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Initialize the PdfFileInfo facade with the PDF file path
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Read and display various metadata properties
            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($"Title          : {pdfInfo.Title}");
            Console.WriteLine($"Author         : {pdfInfo.Author}");
            Console.WriteLine($"Subject        : {pdfInfo.Subject}");
            Console.WriteLine($"Keywords       : {pdfInfo.Keywords}");
            Console.WriteLine($"Creator        : {pdfInfo.Creator}");
            Console.WriteLine($"Producer       : {pdfInfo.Producer}");
            Console.WriteLine($"Creation Date  : {pdfInfo.CreationDate}");
            Console.WriteLine($"Modification Date : {pdfInfo.ModDate}");
            Console.WriteLine($"Number of Pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Is Encrypted   : {pdfInfo.IsEncrypted}");
            Console.WriteLine($"Has Open Password : {pdfInfo.HasOpenPassword}");
            Console.WriteLine($"Has Edit Password : {pdfInfo.HasEditPassword}");
            Console.WriteLine($"Is PDF File    : {pdfInfo.IsPdfFile}");
            Console.WriteLine($"Has Collection : {pdfInfo.HasCollection}");

            // Release resources held by the facade
            pdfInfo.Close();
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., invalid PDF, permission issues)
            Console.Error.WriteLine($"An error occurred while reading PDF metadata: {ex.Message}");
        }
    }
}