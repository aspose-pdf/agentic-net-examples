using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form associated with the document
            Form form = doc.Form;

            // If the document contains no form fields, report and exit
            if (form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Iterate over each field, retrieving its qualified name and current value
            foreach (Field field in form.Fields)
            {
                string fieldName = field.FullName;                     // Fully qualified field name
                string fieldValue = field.Value?.ToString() ?? string.Empty; // Field value (may be null)

                Console.WriteLine($"Field: {fieldName} = \"{fieldValue}\"");
            }

            // No changes are made; therefore no Save() call is required.
        }
    }
}