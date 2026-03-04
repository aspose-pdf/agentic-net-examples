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

        // PdfFileInfo provides access to PDF metadata.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve desired properties.
            string title    = pdfInfo.Title;
            string author   = pdfInfo.Author;
            string subject  = pdfInfo.Subject;
            string keywords = pdfInfo.Keywords;

            // Output the information.
            Console.WriteLine($"Title   : {title}");
            Console.WriteLine($"Author  : {author}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}