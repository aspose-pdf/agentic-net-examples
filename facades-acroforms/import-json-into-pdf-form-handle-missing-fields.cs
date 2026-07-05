using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string srcPdfPath   = "input.pdf";   // source PDF with form fields
        const string jsonDataPath = "data.json";   // JSON file containing field values
        const string outputPdfPath = "output.pdf"; // result PDF after import

        // Verify that required files exist
        if (!File.Exists(srcPdfPath) || !File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine("Source PDF or JSON data file not found.");
            return;
        }

        // Create the Form facade for the source PDF (lifecycle: create + dispose)
        using (Form form = new Form(srcPdfPath))
        {
            // Import JSON data and handle missing fields
            try
            {
                using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportJson(jsonStream); // import operation
                }
            }
            // Aspose.Pdf may throw PdfException when a field referenced in the JSON does not exist.
            // Older versions exposed FormException, which is no longer present. We therefore catch the
            // more general PdfException and extract the missing field name from the exception message.
            catch (PdfException ex)
            {
                string missingField = ExtractMissingFieldName(ex.Message);
                Console.WriteLine($"Missing form field during import: {missingField}");
            }
            // Fallback for any other unexpected exception types.
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during form import: {ex.Message}");
            }

            // Save the updated PDF (lifecycle: save)
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form import completed. Output saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Attempts to extract a field name from an Aspose.Pdf exception message.
    /// The exact wording varies between versions, so a simple regex is used to capture
    /// the first word that looks like a field identifier.
    /// </summary>
    private static string ExtractMissingFieldName(string message)
    {
        if (string.IsNullOrEmpty(message))
            return "<unknown>";

        // Common patterns: "Field 'FieldName' not found" or "Cannot find field FieldName"
        var match = Regex.Match(message, @"'(?<name>[^']+)'|\bfield\s+(?<name>\w+)", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups["name"].Value : "<unknown>";
    }
}
