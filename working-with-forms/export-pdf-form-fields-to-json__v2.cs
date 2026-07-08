using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will contain the form schema
        const string outputJsonPath = "form_schema.json";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare export options (optional – here we request indented JSON for readability)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true
            };

            // Export the form fields to JSON using a file stream (lifecycle rule: use using for the stream)
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields; the method returns serialization results which can be inspected if needed
                IEnumerable<FieldSerializationResult> results = pdfDoc.Form.ExportToJson(jsonStream, jsonOptions);

                // Optionally, iterate over results to report any warnings or errors
                foreach (var result in results)
                {
                    if (result.FieldSerializationStatus != FieldSerializationStatus.Success)
                    {
                        Console.WriteLine($"Field '{result.FieldFullName}' serialization status: {result.FieldSerializationStatus}");
                        foreach (string warning in result.WarningMessages)
                            Console.WriteLine($"  Warning: {warning}");
                        foreach (string error in result.ErrorMessages)
                            Console.WriteLine($"  Error: {error}");
                    }
                }
            }

            // No additional Save() call is required because ExportToJson writes directly to the provided stream
        }

        Console.WriteLine($"Form schema exported to '{outputJsonPath}'.");
    }
}