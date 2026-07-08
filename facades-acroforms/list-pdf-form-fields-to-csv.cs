using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "fields_report.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Bind the document to FormEditor as required
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Use the Form facade to obtain field names
                Form form = new Form(doc);
                string[] fieldNames = form.FieldNames;

                // Write the report to CSV
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    writer.WriteLine("FieldName,FieldType");
                    foreach (string name in fieldNames)
                    {
                        // Retrieve the field type for each field
                        Aspose.Pdf.Facades.FieldType fieldType = form.GetFieldType(name);
                        writer.WriteLine($"{name},{fieldType}");
                    }
                }
            }
        }

        Console.WriteLine($"Form fields report saved to '{outputCsv}'.");
    }
}