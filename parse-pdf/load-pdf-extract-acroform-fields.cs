using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF file containing an AcroForm
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Access the AcroForm of the document
            Form acroForm = doc.Form;

            // If the document has no form or no fields, inform the user
            if (acroForm == null || acroForm.Fields == null || !acroForm.Fields.Any())
            {
                Console.WriteLine("No AcroForm fields found in the document.");
                return;
            }

            // Iterate over all form fields and output their names and values
            foreach (Field field in acroForm.Fields)
            {
                // Some field types expose a Value property; use ToString() for safety
                string value = field?.Value?.ToString() ?? "(null)";
                Console.WriteLine($"Field: {field.FullName}, Value: {value}");
            }
        }
    }
}
