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

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Regular expression that metadata must satisfy (example: alphanumeric, spaces, max 100 chars)
        const string pattern = @"^[\w\s]{0,100}$";
        Regex regex = new Regex(pattern, RegexOptions.Compiled);

        // Metadata values to set
        string title   = "Sample Document Title";
        string author  = "John Doe";
        string subject = "Demo Subject";

        // Validate each metadata value against the regex
        if (!regex.IsMatch(title))
        {
            Console.Error.WriteLine("Title does not match the required pattern.");
            return;
        }
        if (!regex.IsMatch(author))
        {
            Console.Error.WriteLine("Author does not match the required pattern.");
            return;
        }
        if (!regex.IsMatch(subject))
        {
            Console.Error.WriteLine("Subject does not match the required pattern.");
            return;
        }

        // Load the PDF and set metadata using PdfFileInfo (Facades API)
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);   // Load existing PDF
        pdfInfo.Title   = title;      // Set validated metadata
        pdfInfo.Author  = author;
        pdfInfo.Subject = subject;

        // Save the updated PDF to a new file
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}