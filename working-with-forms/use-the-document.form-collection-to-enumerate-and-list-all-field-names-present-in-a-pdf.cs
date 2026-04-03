using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ListPdfFormFields
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm collection
            Form pdfForm = doc.Form;

            // Enumerate all fields via the Fields collection
            Console.WriteLine("Form field names in the PDF:");
            foreach (Field field in pdfForm.Fields) // corrected type
            {
                // PartialName holds the field's identifier
                Console.WriteLine($"- {field.PartialName}");
            }
        }
    }
}
