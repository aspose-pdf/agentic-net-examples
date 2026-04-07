using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class PdfValidator
{
    // Validates required form fields and saves the document if validation passes.
    public static void ValidateAndSave(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all form fields (Field objects)
            foreach (Field field in doc.Form)
            {
                // Check if the field is marked as required
                if (field.Required)
                {
                    // Retrieve the field value; treat null or empty string as not filled
                    string value = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new InvalidOperationException(
                            $"Required field '{field.PartialName}' is not filled.");
                    }
                }
            }

            // All required fields are filled; save the document
            doc.Save(outputPdfPath);
        }
    }

    // Example usage
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "validated_output.pdf";

        try
        {
            ValidateAndSave(inputPath, outputPath);
            Console.WriteLine($"Document saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation failed: {ex.Message}");
        }
    }
}
