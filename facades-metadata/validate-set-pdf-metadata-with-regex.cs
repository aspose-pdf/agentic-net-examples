using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    // Regular expression that metadata must satisfy (alphanumeric and spaces)
    private const string Pattern = @"^[A-Za-z0-9\s]+$";

    static void Main()
    {
        // Input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Metadata values to set
        const string title = "Sample Title 123";
        const string author = "John Doe";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using PdfFileInfo, set metadata, and save the updated file
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Set standard metadata properties after validation
            if (!SetValidatedMetaInfo(info, "Title", title)) return;
            if (!SetValidatedMetaInfo(info, "Author", author)) return;

            // Example of setting a custom metadata property after validation
            const string customKey = "CustomProperty";
            const string customValue = "Custom123";
            SetValidatedMetaInfo(info, customKey, customValue);

            // Save the PDF with the new metadata
            info.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata validated and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Validates a metadata value against the predefined pattern and, if valid, assigns it to the PdfFileInfo instance.
    /// For standard keys (Title, Author) the corresponding strongly‑typed properties are used; otherwise <c>SetMetaInfo</c> is called.
    /// </summary>
    /// <param name="info">The PdfFileInfo instance to modify.</param>
    /// <param name="key">Metadata key (e.g., "Title", "Author", or a custom key).</param>
    /// <param name="value">The value to validate and set.</param>
    /// <returns>True if the value matches the pattern and was set; false otherwise.</returns>
    private static bool SetValidatedMetaInfo(PdfFileInfo info, string key, string value)
    {
        if (!IsValid(value, Pattern))
        {
            Console.Error.WriteLine($"{key} does not match the required pattern.");
            return false;
        }

        // Assign to the appropriate property when possible; fall back to generic metadata.
        switch (key)
        {
            case "Title":
                info.Title = value;
                break;
            case "Author":
                info.Author = value;
                break;
            default:
                info.SetMetaInfo(key, value);
                break;
        }
        return true;
    }

    // Helper method to validate a string against a regular expression
    private static bool IsValid(string value, string pattern)
    {
        return Regex.IsMatch(value ?? string.Empty, pattern);
    }
}
