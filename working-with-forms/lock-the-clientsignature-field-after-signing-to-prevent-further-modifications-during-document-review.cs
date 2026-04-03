using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_locked.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Required file not found.");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Ensure that existing signatures remain valid after saving (if supported by the version)
                // The property may not exist in older versions; keep it guarded by a compile‑time check if needed.
                // doc.Form.SignaturesAppendOnly = true;

                // Retrieve the signature field named "ClientSignature"
                // The Form indexer returns a WidgetAnnotation, so we need an explicit cast to Field.
                Field field = doc.Form["ClientSignature"] as Field;
                if (field is SignatureField sigField)
                {
                    // Create a PKCS#7 signature object using the certificate
                    PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
                    pkcs7.Reason = "Approved";
                    pkcs7.Location = "Company HQ";

                    // Sign the field
                    sigField.Sign(pkcs7);

                    // Lock the field to prevent any further modifications
                    sigField.ReadOnly = true;
                }
                else
                {
                    Console.Error.WriteLine("Signature field 'ClientSignature' not found.");
                }

                // Save the signed and locked document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Document saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
