using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file
        const string pdfPath = "input.pdf";

        // Verify that the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF metadata using PdfFileInfo facade
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Retrieve metadata properties
                string title   = info.Title   ?? "(none)";
                string author  = info.Author  ?? "(none)";
                string subject = info.Subject ?? "(none)";
                string keywords = info.Keywords ?? "(none)";

                // Output the metadata
                Console.WriteLine($"Title   : {title}");
                Console.WriteLine($"Author  : {author}");
                Console.WriteLine($"Subject : {subject}");
                Console.WriteLine($"Keywords: {keywords}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while reading PDF metadata: {ex.Message}");
        }
    }
}