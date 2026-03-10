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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object associated with the document
            Form form = doc.Form;

            // Iterate through all fields in the form
            foreach (Field field in form.Fields)
            {
                // Use FullName if available; otherwise fall back to Name
                string identifier = field.FullName ?? field.Name ?? "(unnamed)";

                // Retrieve the field's value; handle nulls gracefully
                string value = field.Value?.ToString() ?? string.Empty;

                Console.WriteLine($"Field Identifier: {identifier}, Value: {value}");
            }
        }
    }
}