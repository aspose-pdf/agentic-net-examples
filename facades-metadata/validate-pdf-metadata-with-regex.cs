using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    // Regular expression that metadata values must match.
    // Adjust the pattern as needed for your validation rules.
    private const string MetadataPattern = @"^[A-Za-z0-9\s\-\_]+$";

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_validated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create and load the PDF file info facade.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Example metadata values to set.
            string title  = "Sample Document Title";
            string author = "John Doe";
            string subject = "Demo Subject";

            // Validate each value against the regular expression before assigning.
            if (IsValidMetadata(title))
                pdfInfo.Title = title;
            else
                Console.WriteLine("Title does not match the required pattern and will not be set.");

            if (IsValidMetadata(author))
                pdfInfo.Author = author;
            else
                Console.WriteLine("Author does not match the required pattern and will not be set.");

            if (IsValidMetadata(subject))
                pdfInfo.Subject = subject;
            else
                Console.WriteLine("Subject does not match the required pattern and will not be set.");

            // Save the updated PDF with the new metadata.
            pdfInfo.SaveNewInfo(outputPdf);
        }

        Console.WriteLine($"Metadata validation complete. Output saved to '{outputPdf}'.");
    }

    // Helper method to validate a metadata string against the defined regex.
    private static bool IsValidMetadata(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        return Regex.IsMatch(value, MetadataPattern);
    }
}