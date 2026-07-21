using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfPublicationValidator
{
    /// <summary>
    /// Validates that the specified PDF contains all mandatory XMP metadata fields.
    /// If validation succeeds, the PDF is saved to the output path.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the validated PDF will be saved.</param>
    /// <param name="mandatoryFields">Array of XMP field names that must be present (e.g., "dc:title", "dc:creator").</param>
    /// <returns>True if all mandatory fields are present and the PDF was saved; otherwise false.</returns>
    public static bool ValidateAndPublish(string inputPdfPath, string outputPdfPath, string[] mandatoryFields)
    {
        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return false;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Bind the PDF to the XMP metadata facade (facade rule)
                PdfXmpMetadata xmp = new PdfXmpMetadata(pdfDoc);
                xmp.BindPdf(pdfDoc);

                // Check each mandatory XMP field
                foreach (string fieldName in mandatoryFields)
                {
                    if (!xmp.ContainsKey(fieldName))
                    {
                        Console.Error.WriteLine($"Missing mandatory XMP field: {fieldName}");
                        return false; // Validation failed
                    }
                }

                // Optional: perform PDF structural validation (uses Document.Validate)
                // The validation log is written to a temporary file; it can be inspected if needed.
                string validationLog = Path.Combine(Path.GetDirectoryName(outputPdfPath) ?? ".", "validation.log");
                bool isValid = pdfDoc.Validate(validationLog, PdfFormat.PDF_A_1B);
                if (!isValid)
                {
                    Console.Error.WriteLine($"PDF validation failed. See log: {validationLog}");
                    return false;
                }

                // Save the validated PDF (lifecycle rule: use Document.Save)
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF validated and saved to: {outputPdfPath}");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation/publication: {ex.Message}");
            return false;
        }
    }

    // Example usage
    static void Main()
    {
        string inputPath = "source.pdf";
        string outputPath = "published.pdf";

        // Define the XMP fields that must be present
        string[] requiredXmpFields = new[]
        {
            "dc:title",
            "dc:creator",
            "dc:description"
        };

        bool result = ValidateAndPublish(inputPath, outputPath, requiredXmpFields);
        Console.WriteLine(result ? "Publication succeeded." : "Publication aborted due to missing XMP metadata.");
    }
}