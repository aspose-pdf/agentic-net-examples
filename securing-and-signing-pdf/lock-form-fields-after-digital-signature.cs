using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_locked.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Names of form fields that should become read‑only after signing
        string[] fieldsToLock = { "Name", "DateOfBirth", "Address" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a signature field (if not already present) on the first page
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1" // optional name for the field
            };
            doc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // 2. Prepare the digital signature (PKCS#7)
            // -----------------------------------------------------------------
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Approved",
                Location = "New York, USA",
                ContactInfo = "John Doe"
            };

            // -----------------------------------------------------------------
            // 3. Sign the document using the signature field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs7);

            // -----------------------------------------------------------------
            // 4. Lock the specified form fields (make them read‑only)
            // -----------------------------------------------------------------
            foreach (string fieldName in fieldsToLock)
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field.
                Field field = doc.Form[fieldName] as Field;
                if (field != null)
                {
                    field.ReadOnly = true;
                }
                else
                {
                    Console.WriteLine($"Field \"{fieldName}\" not found in the form.");
                }
            }

            // -----------------------------------------------------------------
            // 5. Save the signed and locked PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed and saved to '{outputPdf}'.");
    }
}
