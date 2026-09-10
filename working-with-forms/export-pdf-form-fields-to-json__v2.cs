using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf; // ExportFieldsToJsonOptions resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_schema.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (PDF format does not require explicit load options)
        using (Document doc = new Document(inputPdf))
        {
            // Configure JSON export options
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,          // Produce readable indented JSON
                ExportPasswordValue = false    // Do not include password values in the output
            };

            // Export all form fields to a JSON file.
            // The method returns a collection of FieldSerializationResult objects.
            var results = doc.Form.ExportToJson(outputJson, jsonOptions);

            // Optionally, inspect the serialization results.
            foreach (var result in results)
            {
                Console.WriteLine($"Field: {result.FieldFullName}");
                Console.WriteLine($"Status: {result.FieldSerializationStatus}");
                if (result.WarningMessages.Count > 0)
                {
                    Console.WriteLine("Warnings:");
                    foreach (var warn in result.WarningMessages)
                        Console.WriteLine($"  - {warn}");
                }
                if (result.ErrorMessages.Count > 0)
                {
                    Console.WriteLine("Errors:");
                    foreach (var err in result.ErrorMessages)
                        Console.WriteLine($"  - {err}");
                }
                Console.WriteLine();
            }
        }

        Console.WriteLine($"Form field schema exported to '{outputJson}'.");
    }
}