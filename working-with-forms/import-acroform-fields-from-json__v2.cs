using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath = "form_schema.json";
        const string outputPdf = "generated_form.pdf";

        // Verify that the JSON schema file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Import AcroForm fields defined in the JSON schema
            doc.Form.ImportFromJson(jsonPath);

            // Save the PDF containing the generated form fields
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPdf}'.");
    }
}