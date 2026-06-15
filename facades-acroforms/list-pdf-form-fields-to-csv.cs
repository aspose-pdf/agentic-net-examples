using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "fields_report.csv";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize FormEditor and bind the PDF document
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdfPath);

            // Use the Form facade to access form fields
            using (Form form = new Form(formEditor.Document))
            {
                string[] fieldNames = form.FieldNames;

                // Write field names and their types to a CSV file
                using (StreamWriter writer = new StreamWriter(outputCsvPath))
                {
                    // CSV header
                    writer.WriteLine("FieldName,FieldType");

                    foreach (string fieldName in fieldNames)
                    {
                        // Retrieve the field type for each field
                        FieldType fieldType = form.GetFieldType(fieldName);

                        // Write a CSV line
                        writer.WriteLine($"{fieldName},{fieldType}");
                    }
                }

                Console.WriteLine($"Form field report saved to '{outputCsvPath}'.");
            }

            // No need to call Save on FormEditor because we are only reading the form structure
        }
    }
}