using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade with the PDF file.
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Basic document metadata.
            Console.WriteLine($"Title: {info.Title}");
            Console.WriteLine($"Author: {info.Author}");
            Console.WriteLine($"Subject: {info.Subject}");
            Console.WriteLine($"Keywords: {info.Keywords}");
            Console.WriteLine($"Creator: {info.Creator}");
            Console.WriteLine($"Producer: {info.Producer}");
            Console.WriteLine($"Creation Date: {info.CreationDate}");
            Console.WriteLine($"Modification Date: {info.ModDate}");

            // Structural information.
            Console.WriteLine($"Number of Pages: {info.NumberOfPages}");
            Console.WriteLine($"PDF Version: {info.GetPdfVersion()}");
            Console.WriteLine($"Is Encrypted: {info.IsEncrypted}");
            Console.WriteLine($"Has Open Password: {info.HasOpenPassword}");
            Console.WriteLine($"Has Edit Password: {info.HasEditPassword}");
            Console.WriteLine($"Is PDF File: {info.IsPdfFile}");
            Console.WriteLine($"Has Collection (Portfolio): {info.HasCollection}");

            // Document privileges (e.g., printing, modifying).
            var privilege = info.GetDocumentPrivilege();
            Console.WriteLine($"Document Privilege: {privilege}");
        }
    }
}