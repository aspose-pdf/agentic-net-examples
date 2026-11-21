using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF containing form fields
        const string outputCsv = "form_fields_report.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document using the high‑level Document API.
            Document doc = new Document(inputPdf);

            // Write the report to a CSV file.
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("FieldName,FieldType");

                // Iterate over each form field using the correct Field type.
                foreach (Field field in doc.Form.Fields)
                {
                    // field.PartialName is the field's name.
                    // The concrete runtime type (TextBoxField, CheckboxField, etc.)
                    // tells us the field type.
                    string fieldName = field.PartialName;
                    string fieldType = field.GetType().Name; // e.g., "TextBoxField"
                    writer.WriteLine($"{fieldName},{fieldType}");
                }
            }

            Console.WriteLine($"Form field report generated: {outputCsv}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
