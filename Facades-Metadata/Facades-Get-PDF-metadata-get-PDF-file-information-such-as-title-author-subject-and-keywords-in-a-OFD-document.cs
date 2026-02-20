using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the OFD file
        const string ofdPath = "sample.ofd";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"Error: OFD file not found at '{ofdPath}'.");
            return;
        }

        // Load the OFD document using PdfFileInfo (facade class) and extract metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo(ofdPath))
        {
            // The PdfFileInfo class provides access to common PDF/OFD metadata properties
            string title   = pdfInfo.Title   ?? "(no title)";
            string author  = pdfInfo.Author  ?? "(no author)";
            string subject = pdfInfo.Subject ?? "(no subject)";
            string keywords = pdfInfo.Keywords ?? "(no keywords)";

            // Output the retrieved metadata
            Console.WriteLine("Document Metadata:");
            Console.WriteLine($"Title   : {title}");
            Console.WriteLine($"Author  : {author}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}