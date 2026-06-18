using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access standard metadata via DocumentInfo
            DocumentInfo info = doc.Info;

            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($"Title   : {info.Title}");
            Console.WriteLine($"Author  : {info.Author}");
            Console.WriteLine($"Subject : {info.Subject}");
            Console.WriteLine($"Keywords: {info.Keywords}");
            Console.WriteLine($"Creator : {info.Creator}");
            Console.WriteLine($"Producer: {info.Producer}");
            Console.WriteLine($"CreationDate: {info.CreationDate}");
            Console.WriteLine($"ModDate     : {info.ModDate}");

            // Count embedded file attachments (if any)
            int attachmentCount = doc.EmbeddedFiles?.Count ?? 0;
            Console.WriteLine($"Attachments: {attachmentCount}");
        }
    }
}