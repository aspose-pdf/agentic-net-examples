using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Core API for AcroForm handling

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm associated with the document
            Form acroForm = doc.Form;

            // Output basic information about the form fields
            Console.WriteLine($"Total form fields: {acroForm.Count}");

            // Iterate over each field in the AcroForm
            foreach (Field field in acroForm)
            {
                // Field.Name provides the fully qualified field name
                // field.GetType().Name indicates the specific field type (e.g., TextBoxField, CheckBoxField)
                Console.WriteLine($"Field: {field.Name}, Type: {field.GetType().Name}");
            }
        }
    }
}