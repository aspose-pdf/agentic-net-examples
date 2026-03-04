using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ps"; // Path to the PostScript file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo can be used to read metadata from the input file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve standard metadata properties
            string title   = pdfInfo.Title   ?? string.Empty;
            string author  = pdfInfo.Author  ?? string.Empty;
            string subject = pdfInfo.Subject ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Output the information
            Console.WriteLine($"Title   : {title}");
            Console.WriteLine($"Author  : {author}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}