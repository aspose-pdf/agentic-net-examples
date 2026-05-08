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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object; it may be empty if the PDF has no form fields
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all fields and pick out the signature fields
            foreach (Field field in form)
            {
                if (field is SignatureField signatureField)
                {
                    // FullName provides the qualified field name
                    string fieldName = signatureField.FullName;
                    // PageIndex is 1‑based, matching Aspose.Pdf's page indexing
                    int pageNumber = signatureField.PageIndex;

                    Console.WriteLine($"Signature field: '{fieldName}' on page {pageNumber}");
                }
            }
        }
    }
}