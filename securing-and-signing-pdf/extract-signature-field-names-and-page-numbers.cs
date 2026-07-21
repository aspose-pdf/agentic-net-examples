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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form (collection of fields)
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine("Signature fields found:");
            // Iterate over all fields; filter for SignatureField instances
            foreach (Field field in form)
            {
                if (field is SignatureField sigField)
                {
                    // FullName provides the field's name; PageIndex is 1‑based
                    string fieldName = sigField.FullName;
                    int pageNumber = sigField.PageIndex;
                    Console.WriteLine($"- Name: {fieldName}, Page: {pageNumber}");
                }
            }
        }
    }
}