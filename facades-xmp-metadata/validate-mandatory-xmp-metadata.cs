using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfPublicationValidator
{
    /// <summary>
    /// Validates that the specified XMP metadata fields exist in the PDF.
    /// If all mandatory fields are present, the PDF is saved to the output path.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the validated PDF will be saved.</param>
    /// <param name="mandatoryXmpFields">Array of XMP field names to check (e.g., "dc:title").</param>
    /// <returns>True if validation succeeded and the PDF was saved; otherwise false.</returns>
    public static bool ValidateAndPublish(string inputPdfPath, string outputPdfPath, string[] mandatoryXmpFields)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return false;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind XMP metadata facade to the loaded document
            using (PdfXmpMetadata xmpMetadata = new PdfXmpMetadata())
            {
                xmpMetadata.BindPdf(pdfDoc);

                // Check each mandatory XMP field
                foreach (string fieldName in mandatoryXmpFields)
                {
                    // Get the raw bytes for the specific metadata element
                    byte[] fieldData = xmpMetadata.GetXmpMetadata(fieldName);

                    // If the field is missing or empty, validation fails
                    if (fieldData == null || fieldData.Length == 0)
                    {
                        Console.Error.WriteLine($"Missing mandatory XMP field: {fieldName}");
                        return false;
                    }
                }
            }

            // All mandatory fields are present – save the PDF (lifecycle rule: use Save(string))
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF validated and saved to '{outputPdfPath}'.");
            return true;
        }
    }

    // Example usage
    static void Main()
    {
        string inputPath = "source.pdf";
        string outputPath = "validated_output.pdf";

        // Define the XMP fields that must be present
        string[] requiredFields = new string[]
        {
            "dc:title",
            "dc:creator",
            "dc:description"
        };

        ValidateAndPublish(inputPath, outputPath, requiredFields);
    }
}