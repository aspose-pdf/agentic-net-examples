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

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // ----- Read standard metadata -----
            string title       = doc.Info.Title       ?? "(none)";
            string author      = doc.Info.Author      ?? "(none)";
            string subject     = doc.Info.Subject     ?? "(none)";
            string keywords    = doc.Info.Keywords    ?? "(none)";
            string creator     = doc.Info.Creator     ?? "(none)";
            string producer    = doc.Info.Producer    ?? "(none)";
            string creationDt  = doc.Info.CreationDate != DateTime.MinValue ? doc.Info.CreationDate.ToString() : "(none)";
            string modDt       = doc.Info.ModDate != DateTime.MinValue ? doc.Info.ModDate.ToString() : "(none)";

            // ----- Count embedded file attachments -----
            // In Aspose.Pdf the collection is called EmbeddedFiles, not Attachments.
            int attachmentCount = doc.EmbeddedFiles?.Count ?? 0;

            // ----- Output the report -----
            Console.WriteLine("PDF Metadata Report");
            Console.WriteLine("-------------------");
            Console.WriteLine($"Title            : {title}");
            Console.WriteLine($"Author           : {author}");
            Console.WriteLine($"Subject          : {subject}");
            Console.WriteLine($"Keywords         : {keywords}");
            Console.WriteLine($"Creator          : {creator}");
            Console.WriteLine($"Producer         : {producer}");
            Console.WriteLine($"Creation Date    : {creationDt}");
            Console.WriteLine($"Modification Date: {modDt}");
            Console.WriteLine($"Attachment Count : {attachmentCount}");
        }
    }
}
