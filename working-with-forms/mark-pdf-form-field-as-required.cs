using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_required.pdf";
        const string fieldName = "MyTextField"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Locate the field by its partial or full name
            if (doc.Form.HasField(fieldName))
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field to access form‑specific members
                Field? formField = doc.Form[fieldName] as Field;
                if (formField == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' exists but is not a form field.");
                    return;
                }

                // Mark the field as required; validation will fail if left empty
                formField.Required = true;

                // Optionally, make the field visually indicate that it is required (e.g., red border)
                // formField.Border = new Border(formField) { Width = 1 };
                // formField.Color = Aspose.Pdf.Color.Red;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with required field: '{outputPath}'.");
    }
}
