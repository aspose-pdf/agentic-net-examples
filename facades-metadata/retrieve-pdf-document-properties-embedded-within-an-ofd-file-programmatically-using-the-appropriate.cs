using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the input OFD file
        const string ofdPath = "input.ofd";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load the OFD file as a PDF document using OfdLoadOptions
        var ofdLoadOptions = new OfdLoadOptions();
        using (var pdfDoc = new Document(ofdPath, ofdLoadOptions))
        {
            // Initialize PdfFileInfo with the loaded Document
            var info = new PdfFileInfo(pdfDoc);

            // Retrieve and display common PDF properties
            Console.WriteLine("=== PDF properties extracted from OFD ===");
            Console.WriteLine($"Title          : {info.Title}");
            Console.WriteLine($"Author         : {info.Author}");
            Console.WriteLine($"Creator        : {info.Creator}");
            Console.WriteLine($"Subject        : {info.Subject}");
            Console.WriteLine($"Keywords       : {info.Keywords}");
            Console.WriteLine($"Creation Date  : {info.CreationDate}");
            Console.WriteLine($"Modification Date: {info.ModDate}");
            Console.WriteLine($"Number of Pages: {info.NumberOfPages}");
            Console.WriteLine($"PDF Version    : {info.GetPdfVersion()}");
            Console.WriteLine($"Is Encrypted   : {info.IsEncrypted}");
            Console.WriteLine($"Has Open Password: {info.HasOpenPassword}");
            Console.WriteLine($"Has Edit Password: {info.HasEditPassword}");
        }
    }
}