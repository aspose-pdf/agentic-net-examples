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
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine("Signature fields found:");

            // Iterate over all fields and select only SignatureField instances
            foreach (var field in doc.Form.Fields)
            {
                if (field is SignatureField signature)
                {
                    // FullName provides the qualified field name
                    string fieldName = signature.FullName;

                    // PageIndex is 1‑based according to Aspose.Pdf conventions
                    int pageNumber = signature.PageIndex;

                    Console.WriteLine($"- Name: {fieldName}, Page: {pageNumber}");
                }
            }
        }
    }
}