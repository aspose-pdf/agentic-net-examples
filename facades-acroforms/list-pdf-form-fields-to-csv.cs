using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF with form fields
        const string outputCsvPath = "form_fields.csv"; // CSV report file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and bind it to FormEditor (required by the task)
        using (Document doc = new Document(inputPdfPath))
        using (FormEditor formEditor = new FormEditor(doc))
        {
            // FormEditor works on the same Document instance; we can use Form facade to query fields
            Form formFacade = new Form(doc);

            // Retrieve all field names
            string[] fieldNames = formFacade.FieldNames;

            // Write the report to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // CSV header
                writer.WriteLine("FieldName,FieldType");

                // Iterate over each field, obtain its type, and write a line to the CSV
                foreach (string fieldName in fieldNames)
                {
                    // Get the field type using the Form facade
                    FieldType fieldType = formFacade.GetFieldType(fieldName);

                    // Write CSV line (field name and its type as string)
                    writer.WriteLine($"{fieldName},{fieldType}");
                }
            }

            // Save the (unchanged) PDF if needed; here we just ensure proper disposal
            // (FormEditor inherits SaveableFacade, but no modifications were made)
            // formEditor.Save(); // not required for this read‑only operation
        }

        Console.WriteLine($"Form field report generated: {outputCsvPath}");
    }
}