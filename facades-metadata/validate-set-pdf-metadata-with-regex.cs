using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Example regular expression: allow letters, numbers, spaces, hyphens, and underscores
        const string regexPattern = @"^[\w\s\-]+$";

        var metadata = new Dictionary<string, string>
        {
            { "Title", "Sample Document" },
            { "Author", "John Doe" },
            { "Subject", "Metadata Validation" },
            { "Keywords", "Aspose PDF, Validation" },
            // Add custom metadata if needed
            // { "CustomKey", "CustomValue" }
        };

        SetMetadataWithValidation(inputPath, outputPath, regexPattern, metadata);
    }

    static void SetMetadataWithValidation(string inputPath, string outputPath, string regexPattern, Dictionary<string, string> meta)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Regex regex = new Regex(regexPattern, RegexOptions.Compiled);

        // Load the PDF using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            foreach (var kvp in meta)
            {
                // Validate value against the regular expression
                if (!regex.IsMatch(kvp.Value))
                {
                    Console.Error.WriteLine($"Metadata '{kvp.Key}' value '{kvp.Value}' does not match the pattern.");
                    continue;
                }

                // Set known standard properties; unknown keys are treated as custom metadata
                switch (kvp.Key)
                {
                    case "Title":
                        pdfInfo.Title = kvp.Value;
                        break;
                    case "Author":
                        pdfInfo.Author = kvp.Value;
                        break;
                    case "Subject":
                        pdfInfo.Subject = kvp.Value;
                        break;
                    case "Keywords":
                        pdfInfo.Keywords = kvp.Value;
                        break;
                    case "Creator":
                        pdfInfo.Creator = kvp.Value;
                        break;
                    case "CreationDate":
                        if (DateTime.TryParse(kvp.Value, out DateTime creation))
                            // PdfFileInfo expects a PDF‑date formatted string, e.g., "yyyyMMddHHmmss"
                            pdfInfo.CreationDate = creation.ToString("yyyyMMddHHmmss");
                        else
                            Console.Error.WriteLine($"Invalid date format for CreationDate: {kvp.Value}");
                        break;
                    case "ModDate":
                        if (DateTime.TryParse(kvp.Value, out DateTime mod))
                            pdfInfo.ModDate = mod.ToString("yyyyMMddHHmmss");
                        else
                            Console.Error.WriteLine($"Invalid date format for ModDate: {kvp.Value}");
                        break;
                    default:
                        // Custom metadata entry
                        pdfInfo.SetMetaInfo(kvp.Key, kvp.Value);
                        break;
                }
            }

            // Save the updated PDF with new metadata
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata validated and saved to '{outputPath}'.");
    }
}
