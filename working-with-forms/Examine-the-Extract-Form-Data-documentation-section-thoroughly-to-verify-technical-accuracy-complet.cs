using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class ExtractFormDataDemo
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonOutput = "formdata.json";
        const string xfdfOutput = "annotations.xfdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the form object
            Form form = doc.Form;

            // Check whether the document contains any form fields
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
            }
            else
            {
                Console.WriteLine($"Form contains {form.Count} fields:");

                // Iterate through each field and display its name and current value
                foreach (Field field in form)
                {
                    string fieldName = field.FullName;
                    string fieldValue = field.Value?.ToString() ?? "<empty>";
                    Console.WriteLine($"  Name: {fieldName}, Value: {fieldValue}");
                }

                // Export form fields to a JSON file using default options
                var jsonOptions = new ExportFieldsToJsonOptions();
                form.ExportToJson(jsonOutput, jsonOptions);
                Console.WriteLine($"Form data exported to JSON: {jsonOutput}");
            }

            // Export all annotations (including form field annotations) to XFDF
            doc.ExportAnnotationsToXfdf(xfdfOutput);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfOutput}");

            // OPTIONAL: Extract raw text from the first page using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[1].Accept(absorber);
            string pageText = absorber.Text;
            Console.WriteLine($"First 200 characters of page 1 text: {pageText.Substring(0, Math.Min(200, pageText.Length))}");
        }
    }
}