using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object; it may be null if the PDF has no AcroForm
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields present in the document.");
                return;
            }

            Console.WriteLine("Signature fields found:");

            // Iterate over all fields in the form
            foreach (Field field in form)
            {
                // Identify signature fields via type checking
                if (field is SignatureField sigField)
                {
                    // FullName provides the fully qualified field name; fallback to Name if null
                    string fieldName = sigField.FullName ?? sigField.Name ?? "(unnamed)";

                    // PageIndex is 1‑based according to Aspose.Pdf rules
                    int pageNumber = sigField.PageIndex;

                    Console.WriteLine($"- Name: {fieldName}, Page: {pageNumber}");
                }
            }
        }
    }
}