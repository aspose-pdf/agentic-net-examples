using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string jsonPath = "form_schema.json";
        const string outputPdf = "generated_form.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema file not found: {jsonPath}");
            return;
        }

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the form fields
            doc.Pages.Add();

            // Import AcroForm fields from the JSON schema
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the PDF with the imported form fields
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported form fields saved to '{outputPdf}'.");
    }
}