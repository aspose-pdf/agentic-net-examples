using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with form fields
        const string outputCsv = "fields_report.csv"; // CSV report file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Bind the document to FormEditor (required by the task)
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // FormEditor does not expose field information, so use Form facade
                using (Form form = new Form(doc))
                {
                    string[] fieldNames = form.FieldNames;

                    using (StreamWriter writer = new StreamWriter(outputCsv))
                    {
                        // CSV header
                        writer.WriteLine("FieldName,FieldType");

                        // Enumerate each field, obtain its type, and write to CSV
                        foreach (string name in fieldNames)
                        {
                            // GetFieldType returns an enum value describing the field type
                            var fieldType = form.GetFieldType(name);
                            writer.WriteLine($"{name},{fieldType}");
                        }
                    }

                    Console.WriteLine($"Form fields report saved to '{outputCsv}'.");
                }
            }
        }
    }
}