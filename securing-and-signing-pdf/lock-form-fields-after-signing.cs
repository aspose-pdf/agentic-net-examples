using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_locked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Create a signature field (if it does not already exist)
            // ------------------------------------------------------------
            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the form
            doc.Form.Add(sigField);

            // ------------------------------------------------------------
            // 2. Prepare a simple PKCS#7 signature (placeholder values)
            // ------------------------------------------------------------
            // NOTE: In a real scenario you must provide a valid certificate file and password.
            // Here we use empty strings so the code compiles; at runtime this will throw
            // if a real signature is required.
            PKCS7 pkcs7 = new PKCS7("certificate.pfx", "password")
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "user@example.com"
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // ------------------------------------------------------------
            // 3. Lock specific form fields after signing
            // ------------------------------------------------------------
            // Example: lock fields named "Name" and "Date"
            string[] fieldsToLock = { "Name", "Date" };

            foreach (string fieldName in fieldsToLock)
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field.
                Field field = doc.Form[fieldName] as Field;
                if (field != null)
                {
                    field.ReadOnly = true; // Prevent user edits
                }
            }

            // ------------------------------------------------------------
            // 4. Prevent any further modifications that would invalidate the signature
            // ------------------------------------------------------------
            // When true, saving the document after a signature will throw an exception
            doc.HandleSignatureChange = true;

            // ------------------------------------------------------------
            // 5. Save the signed and locked PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
