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
        const string titleValue = "Sample Title";
        const string authorValue = "John Doe";
        const string customKey = "ProjectCode";
        const string customValue = "PRJ12345";
        const string pattern = "^[A-Za-z0-9 ]+$"; // allow letters, digits and spaces only

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Validate metadata values against the regular expression
        bool isTitleValid = Regex.IsMatch(titleValue, pattern);
        bool isAuthorValid = Regex.IsMatch(authorValue, pattern);
        bool isCustomValid = Regex.IsMatch(customValue, pattern);

        if (!isTitleValid)
        {
            Console.Error.WriteLine("Title does not match the required pattern.");
        }
        if (!isAuthorValid)
        {
            Console.Error.WriteLine("Author does not match the required pattern.");
        }
        if (!isCustomValid)
        {
            Console.Error.WriteLine("Custom metadata value does not match the required pattern.");
        }

        // Proceed only if all values are valid
        if (isTitleValid && isAuthorValid && isCustomValid)
        {
            try
            {
                using (Document document = new Document(inputPath))
                {
                    // Initialize PdfFileInfo facade on the opened document
                    PdfFileInfo pdfInfo = new PdfFileInfo(document);
                    pdfInfo.UseStrictValidation = true;

                    // Set standard metadata properties
                    pdfInfo.Title = titleValue;
                    pdfInfo.Author = authorValue;

                    // Set a custom metadata entry
                    pdfInfo.SetMetaInfo(customKey, customValue);

                    // Save the updated PDF to a new file
                    pdfInfo.SaveNewInfo(outputPath);
                }

                Console.WriteLine($"Metadata validated and saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
            }
        }
        else
        {
            Console.Error.WriteLine("One or more metadata values failed validation. No changes were made.");
        }
    }
}