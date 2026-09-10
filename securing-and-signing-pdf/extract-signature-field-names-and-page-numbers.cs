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

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Access the form (may be null if no AcroForm present)
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all fields and pick out signature fields
            foreach (Field field in form)
            {
                if (field is SignatureField sigField)
                {
                    // FullName provides the field's name
                    string fieldName = sigField.FullName;
                    // PageIndex returns the 1‑based page number containing the field
                    int pageNumber = sigField.PageIndex;
                    Console.WriteLine($"Signature field \"{fieldName}\" is on page {pageNumber}.");
                }
            }
        }
    }
}