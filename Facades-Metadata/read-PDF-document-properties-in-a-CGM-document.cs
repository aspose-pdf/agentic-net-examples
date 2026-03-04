using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file as a PDF document using CgmLoadOptions
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // PdfFileInfo provides access to PDF metadata properties
            using (PdfFileInfo info = new PdfFileInfo(doc))
            {
                Console.WriteLine($"Title: {info.Title}");
                Console.WriteLine($"Author: {info.Author}");
                Console.WriteLine($"Subject: {info.Subject}");
                Console.WriteLine($"Keywords: {info.Keywords}");
                Console.WriteLine($"Creator: {info.Creator}");
                Console.WriteLine($"Producer: {info.Producer}");
                Console.WriteLine($"CreationDate: {info.CreationDate}");
                Console.WriteLine($"ModDate: {info.ModDate}");
                Console.WriteLine($"Number of pages: {info.NumberOfPages}");
                Console.WriteLine($"PDF version: {info.GetPdfVersion()}");
                Console.WriteLine($"Is encrypted: {info.IsEncrypted}");
                Console.WriteLine($"Is PDF file: {info.IsPdfFile}");
            }
        }
    }
}