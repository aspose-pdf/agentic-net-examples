using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string jsonPath = "formFields.json";
        const string outputPdf = "generated.pdf";

        // Verify that the JSON schema file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Create a new PDF document (empty) and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page – form fields need at least one page to be placed on
            doc.Pages.Add();

            // Import AcroForm fields from the JSON schema into the document's form
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the PDF with the newly created form fields
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported AcroForm fields saved to '{outputPdf}'.");
    }
}