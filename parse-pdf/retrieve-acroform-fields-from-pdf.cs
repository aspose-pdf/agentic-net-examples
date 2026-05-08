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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            Form acroForm = doc.Form;

            // Retrieve all fields in the lowest level of the form hierarchy
            Field[] fields = acroForm.Fields;

            Console.WriteLine($"Total AcroForm fields: {fields.Length}");

            // Iterate over each field and display its name and current value
            foreach (Field field in fields)
            {
                // Field.Name gives the partial name; FullName gives the qualified name
                string fieldName = field.FullName ?? field.Name ?? "(unnamed)";
                string fieldValue = field.Value?.ToString() ?? "(no value)";

                Console.WriteLine($"Field: {fieldName}, Value: {fieldValue}");
            }
        }
    }
}