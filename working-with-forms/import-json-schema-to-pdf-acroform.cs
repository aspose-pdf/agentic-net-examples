using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath   = "form_schema.json";
        const string outputPath = "generated_form.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Import AcroForm fields from the JSON schema file
            doc.Form.ImportFromJson(jsonPath);
            // Alternative using a stream:
            // using (FileStream fs = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            // {
            //     doc.Form.ImportFromJson(fs);
            // }

            // Save the PDF with the generated form fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPath}'.");
    }
}