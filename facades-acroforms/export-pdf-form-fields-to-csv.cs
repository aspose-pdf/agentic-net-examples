using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "form_fields_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Retrieve all form fields from the document
                var formFields = pdfDoc.Form?.Fields;

                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("FieldName,FieldType");

                    if (formFields != null)
                    {
                        // Iterate over each field and write its name and type
                        foreach (var field in formFields)
                        {
                            // Most fields expose their name via PartialName; fallback to Name if null
                            string fieldName = field.PartialName ?? field.Name ?? string.Empty;
                            // Use the concrete .NET type name as the field type (e.g., TextBoxField, CheckBoxField)
                            string fieldType = field.GetType().Name;
                            writer.WriteLine($"{fieldName},{fieldType}");
                        }
                    }
                }

                Console.WriteLine($"Form fields report saved to '{outputCsvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
