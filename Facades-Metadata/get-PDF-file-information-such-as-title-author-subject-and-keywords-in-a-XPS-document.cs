using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xpsPath = "output.xps";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF metadata using the PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

        // Retrieve required information
        string title    = pdfInfo.Title;
        string author   = pdfInfo.Author;
        string subject  = pdfInfo.Subject;
        string keywords = pdfInfo.Keywords;

        Console.WriteLine($"Title   : {title}");
        Console.WriteLine($"Author  : {author}");
        Console.WriteLine($"Subject : {subject}");
        Console.WriteLine($"Keywords: {keywords}");

        // Load the full PDF document to convert it to XPS
        using (Document doc = new Document(pdfPath))
        {
            // Save as XPS – must use explicit SaveOptions for non‑PDF formats
            XpsSaveOptions xpsOptions = new XpsSaveOptions();
            doc.Save(xpsPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {xpsPath}");
    }
}