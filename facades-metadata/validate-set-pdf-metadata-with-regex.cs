using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Example regular expression: only letters, digits and spaces
        const string pattern = @"^[A-Za-z0-9\s]+$";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo();

        // Load the PDF document into the facade
        pdfInfo.BindPdf(inputPath);

        // Validate and set the Title metadata
        string title = "Sample Title 123";
        if (Regex.IsMatch(title, pattern))
        {
            pdfInfo.Title = title;
        }
        else
        {
            Console.WriteLine("Title does not match the required pattern.");
        }

        // Validate and set the Author metadata
        string author = "John Doe";
        if (Regex.IsMatch(author, pattern))
        {
            pdfInfo.Author = author;
        }
        else
        {
            Console.WriteLine("Author does not match the required pattern.");
        }

        // Example of setting a custom metadata property after validation
        string customKey   = "CustomProperty";
        string customValue = "Value123";
        if (Regex.IsMatch(customValue, pattern))
        {
            pdfInfo.SetMetaInfo(customKey, customValue);
        }
        else
        {
            Console.WriteLine($"Custom value for '{customKey}' does not match the pattern.");
        }

        // Save the updated PDF with the new metadata
        pdfInfo.SaveNewInfo(outputPath);
        pdfInfo.Close();

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}