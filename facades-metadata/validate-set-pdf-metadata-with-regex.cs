using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Regular expression defining allowed metadata format (alphanumeric and spaces, 1‑100 chars)
        const string pattern = @"^[A-Za-z0-9\s]{1,100}$";

        // Example metadata values to be set
        string title   = "Sample Document Title";
        string author  = "John Doe";
        string subject = "Demo Subject";

        // Validate each metadata value against the regex
        if (!Regex.IsMatch(title, pattern))
        {
            Console.Error.WriteLine("Invalid title format.");
            return;
        }
        if (!Regex.IsMatch(author, pattern))
        {
            Console.Error.WriteLine("Invalid author format.");
            return;
        }
        if (!Regex.IsMatch(subject, pattern))
        {
            Console.Error.WriteLine("Invalid subject format.");
            return;
        }

        // Load PDF file information using Aspose.Pdf.Facades
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Set metadata after successful validation
        pdfInfo.Title   = title;
        pdfInfo.Author  = author;
        pdfInfo.Subject = subject;

        // Save the updated PDF to a new file
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}