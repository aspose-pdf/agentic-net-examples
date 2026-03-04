using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for accessing PDF file information

class Program
{
    static void Main()
    {
        // Path to the input file (PDF or PCL that can be interpreted as PDF)
        const string inputPath = "input.pcl";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo works with PDF files. If the source is a PCL file,
        // Aspose.Pdf can treat it as a PDF when loading via the facade.
        // The constructor that accepts a file path is sufficient.
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Retrieve standard document properties
            string title   = info.Title   ?? string.Empty;
            string author  = info.Author  ?? string.Empty;
            string subject = info.Subject ?? string.Empty;
            string keywords = info.Keywords ?? string.Empty;

            Console.WriteLine($"Title   : {title}");
            Console.WriteLine($"Author  : {author}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}