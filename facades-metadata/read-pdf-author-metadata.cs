using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document using the high‑level API
        Document pdfDocument = new Document(pdfPath);

        // Retrieve the Author metadata from the document information dictionary
        string author = pdfDocument.Info.Author;
        Console.WriteLine($"Author: {author}");
    }
}
