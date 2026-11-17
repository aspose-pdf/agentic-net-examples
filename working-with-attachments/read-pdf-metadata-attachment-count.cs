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
            string title            = doc.Info.Title ?? "(none)";
            string author           = doc.Info.Author ?? "(none)";
            string subject          = doc.Info.Subject ?? "(none)";
            string keywords         = doc.Info.Keywords ?? "(none)";
            string creator          = doc.Info.Creator ?? "(none)";
            string producer         = doc.Info.Producer ?? "(none)";

            // Creation and modification dates are DateTime values. Convert them to a friendly string.
            string creationDate     = DateTimeToStringOrDefault(doc.Info.CreationDate);
            string modificationDate = DateTimeToStringOrDefault(doc.Info.ModDate);

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
            Console.WriteLine($"Attachment Count: {attachmentCount}");
        }
    }

    // Convert a DateTime to a readable string, falling back to "(none)" when the value is not set.
    private static string DateTimeToStringOrDefault(DateTime dt)
    {
        // Aspose.Pdf uses DateTime.MinValue to represent an unset date.
        return dt == DateTime.MinValue ? "(none)" : dt.ToString("o"); // ISO 8601 format
    }
}
