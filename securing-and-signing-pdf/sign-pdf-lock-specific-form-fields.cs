using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_locked.pdf";
        const string certPath   = "certificate.pfx";
        const string certPwd    = "password";

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
            // -------------------------------------------------
            // 1. Create a signature field (if not already present)
            // -------------------------------------------------
            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            // Add the signature field to the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            doc.Form.Add(sigField);

            // -------------------------------------------------
            // 2. Sign the document using a certificate
            // -------------------------------------------------
            // Use a concrete signature class (PKCS7) – Signature is abstract
            PKCS7 pkcs7 = new PKCS7(certPath, certPwd);
            pkcs7.Reason   = "Document signed";
            pkcs7.Location = "My Company";
            // Apply the signature to the field
            sigField.Sign(pkcs7);

            // -------------------------------------------------
            // 3. Lock specific form fields after signing
            // -------------------------------------------------
            string[] fieldsToLock = { "Name", "Date", "Amount" };
            foreach (string fieldName in fieldsToLock)
            {
                // The Form indexer returns a WidgetAnnotation; cast to Field
                Field field = doc.Form[fieldName] as Field;
                if (field != null)
                {
                    field.ReadOnly = true;
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' not found in the form.");
                }
            }

            // -------------------------------------------------
            // 4. Save the signed and locked PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'. Specified fields are now read‑only.");
    }
}
