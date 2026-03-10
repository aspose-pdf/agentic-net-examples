using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API (Document, XpsLoadOptions)
using Aspose.Pdf.Facades;        // Facade API (PdfFileInfo)

class Program
{
    static void Main()
    {
        const string xpsPath = "input.xps";

        // Verify the XPS file exists
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"File not found: {xpsPath}");
            return;
        }

        // Load the XPS document as a PDF using XpsLoadOptions
        using (Document doc = new Document(xpsPath, new XpsLoadOptions()))
        {
            // PdfFileInfo provides access to standard PDF metadata properties
            PdfFileInfo info = new PdfFileInfo(doc);

            // Retrieve required metadata fields
            string title    = info.Title;
            string author   = info.Author;
            string subject  = info.Subject;
            string keywords = info.Keywords;

            // Output the metadata
            Console.WriteLine($"Title   : {title}");
            Console.WriteLine($"Author  : {author}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}