using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ofd";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the OFD file into a Document (OF D is supported as an input format)
        using (Document doc = new Document(inputPath, new OfdLoadOptions()))
        {
            // Initialize PdfFileInfo with the loaded Document
            PdfFileInfo fileInfo = new PdfFileInfo(doc);

            // Extract standard PDF metadata properties
            string title    = fileInfo.Title;
            string author   = fileInfo.Author;
            string subject  = fileInfo.Subject;
            string keywords = fileInfo.Keywords;

            Console.WriteLine($"Title   : {title}");
            Console.WriteLine($"Author  : {author}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}