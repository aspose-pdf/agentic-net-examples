using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Standard document metadata (fallback to "(none)" if missing)
            string title   = doc.Info.Title ?? "(none)";
            string author  = doc.Info.Author ?? "(none)";
            string subject = doc.Info.Subject ?? "(none)";
            string keywords = doc.Info.Keywords ?? "(none)";
            string creator = doc.Info.Creator ?? "(none)";
            string producer = doc.Info.Producer ?? "(none)";

            // Creation and modification dates are non‑nullable DateTime values.
            // Aspose.Pdf returns DateTime.MinValue when the value is not set.
            string creationDate = doc.Info.CreationDate != DateTime.MinValue
                ? doc.Info.CreationDate.ToString("u")
                : "(none)";
            string modificationDate = doc.Info.ModDate != DateTime.MinValue
                ? doc.Info.ModDate.ToString("u")
                : "(none)";

            // Count embedded file attachments (if any)
            int attachmentCount = doc.EmbeddedFiles?.Count ?? 0;

            // Output a simple report to the console
            Console.WriteLine("PDF Metadata Report");
            Console.WriteLine("-------------------");
            Console.WriteLine($"File: {pdfPath}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Author: {author}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Keywords: {keywords}");
            Console.WriteLine($"Creator: {creator}");
            Console.WriteLine($"Producer: {producer}");
            Console.WriteLine($"Creation Date: {creationDate}");
            Console.WriteLine($"Modification Date: {modificationDate}");
            Console.WriteLine($"Page Count: {doc.Pages.Count}");
            Console.WriteLine($"Attachment Count: {attachmentCount}");
        }
    }
}
