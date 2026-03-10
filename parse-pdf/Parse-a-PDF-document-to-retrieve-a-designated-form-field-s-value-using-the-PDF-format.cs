using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // path to the PDF file
        const string fieldName = "MyField";     // name of the form field to read

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Verify that the document actually contains form fields
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("The document does not contain any form fields.");
                return;
            }

            // Retrieve the specific field by its full name and cast to Field
            Field? field = form[fieldName] as Field;
            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' was not found or is not a form field.");
                return;
            }

            // Extract the field's value (Value may be null, so use null‑conditional operator)
            string value = field.Value?.ToString() ?? string.Empty;
            Console.WriteLine($"Value of field '{fieldName}': {value}");
        }
    }
}
