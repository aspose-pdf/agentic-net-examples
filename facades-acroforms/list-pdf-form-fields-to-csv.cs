using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvReportPath = "fields_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // FormEditor implements IDisposable, so use a using block for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the PDF document to the editor
            formEditor.BindPdf(inputPdfPath);

            // Access the underlying Document object
            Document doc = formEditor.Document;

            // Create a Form facade based on the same Document to query field information
            Form formFacade = new Form(doc);

            // Retrieve all field names
            string[] fieldNames = formFacade.FieldNames;

            // Write the report to a CSV file
            using (StreamWriter writer = new StreamWriter(csvReportPath))
            {
                // Header row
                writer.WriteLine("FieldName,FieldType");

                // Iterate over each field, get its type, and write to CSV
                foreach (string fieldName in fieldNames)
                {
                    // GetFieldType returns a FieldType enum value
                    var fieldType = formFacade.GetFieldType(fieldName);
                    writer.WriteLine($"{fieldName},{fieldType}");
                }
            }

            // No modifications are made, so just close the editor
            formEditor.Close();
        }

        Console.WriteLine($"Form fields report saved to '{csvReportPath}'.");
    }
}