using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists. If it does not, create an empty PDF so that PdfFileInfo can be initialized.
        if (!File.Exists(inputPath))
        {
            // Create a minimal PDF document.
            var emptyDoc = new Document();
            emptyDoc.Pages.Add(); // add a blank page (optional, but makes the file a valid PDF)
            emptyDoc.Save(inputPath);
        }

        // Define a regular expression that allows only letters, digits and spaces.
        // Adjust the pattern as needed for your validation rules.
        const string pattern = "^[A-Za-z0-9\\s]+$"; // escaped backslash for \s
        var regex = new Regex(pattern);

        // Example metadata values to be set.
        string title   = "Sample Document";
        string author  = "John Doe";
        string subject = "Test Subject";

        // Validate each metadata value against the regex.
        if (!regex.IsMatch(title) || !regex.IsMatch(author) || !regex.IsMatch(subject))
        {
            Console.Error.WriteLine("One or more metadata values failed validation.");
            return;
        }

        // Load the PDF, set metadata, and save the updated file.
        using (var pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Title   = title;
            pdfInfo.Author  = author;
            pdfInfo.Subject = subject;

            // Save the PDF with the new metadata to a new file.
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata successfully updated and saved to '{outputPath}'.");
    }
}
