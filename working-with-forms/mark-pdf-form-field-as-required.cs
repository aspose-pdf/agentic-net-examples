using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Verify that the field exists
            if (form.HasField(fieldName))
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field
                Field? field = form[fieldName] as Field;
                if (field != null)
                {
                    // Mark the field as required
                    field.Required = true;
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' exists but is not a supported form field type.");
                }
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with required field: '{outputPath}'.");
    }
}