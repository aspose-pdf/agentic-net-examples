using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            Form pdfForm = doc.Form;
            if (pdfForm == null || pdfForm.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all fields and output their full names and current values
            foreach (Field field in pdfForm.Fields)
            {
                // FullName provides the hierarchical name of the field
                // Value holds the current field value (may be null for empty fields)
                string name = field.FullName;
                string value = field.Value?.ToString() ?? "(empty)";

                Console.WriteLine($"{name}: {value}");
            }
        }
    }
}