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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all fields and pick out the signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField signature)
                {
                    // FullName gives the qualified field name
                    string fieldName = signature.FullName;

                    // PageIndex returns the page number containing the field (1‑based)
                    int pageNumber = signature.PageIndex;

                    Console.WriteLine($"Signature Field: '{fieldName}' on page {pageNumber}");
                }
            }
        }
    }
}