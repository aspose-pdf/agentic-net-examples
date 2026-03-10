using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_filled.pdf";
        const string jsonExportPath = "form_fields.json";
        const string jsonImportPath = "form_fields_to_import.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the Form object
            Form pdfForm = doc.Form;

            // Iterate over all fields and display basic information
            Console.WriteLine("Existing form fields:");
            foreach (Field field in pdfForm)
            {
                Console.WriteLine($"- FullName: {field.FullName}, Value: {field.Value}");
            }

            // Example: set a value for a text box field named "Name"
            const string targetFieldName = "Name";
            if (pdfForm.HasField(targetFieldName))
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field safely
                Field? field = pdfForm[targetFieldName] as Field;
                if (field is TextBoxField textBox)
                {
                    textBox.Value = "John Doe";
                    Console.WriteLine($"Set value of field '{targetFieldName}' to 'John Doe'.");
                }
                else if (field != null)
                {
                    // Field exists but is not a TextBoxField – handle other types if needed
                    Console.WriteLine($"Field '{targetFieldName}' exists but is not a TextBoxField.");
                }
                else
                {
                    // This should not happen because HasField returned true, but guard against unexpected annotation types
                    Console.WriteLine($"Field '{targetFieldName}' could not be cast to a form Field.");
                }
            }

            // Export all form fields to JSON (including current values)
            using (FileStream exportStream = new FileStream(jsonExportPath, FileMode.Create, FileAccess.Write))
            {
                pdfForm.ExportToJson(exportStream);
                Console.WriteLine($"Form fields exported to JSON file: {jsonExportPath}");
            }

            // If you have a JSON file with field values to import, use ImportFromJson
            if (File.Exists(jsonImportPath))
            {
                using (FileStream importStream = new FileStream(jsonImportPath, FileMode.Open, FileAccess.Read))
                {
                    pdfForm.ImportFromJson(importStream);
                    Console.WriteLine($"Form fields imported from JSON file: {jsonImportPath}");
                }
            }

            // Optionally flatten the form (remove interactive fields, keep appearance)
            // Uncomment the following line if you want a non‑editable PDF
            // pdfForm.Flatten();

            // Save the modified document
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved as: {outputPdf}");
        }
    }
}
