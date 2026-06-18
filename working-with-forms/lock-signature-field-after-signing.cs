using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF containing the signature field
        const string outputPdf  = "locked_output.pdf"; // Resulting PDF with the field locked
        const string certPath   = "certificate.pfx";   // PFX file with signing certificate
        const string certPass   = "password";          // Password for the PFX file
        const string fieldName  = "ClientSignature";   // Name of the signature field to lock

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
            // Retrieve the field by name and safely cast to Aspose.Pdf.Forms.Field
            Field field = doc.Form[fieldName] as Field;
            if (field == null || !(field is SignatureField sigField))
            {
                Console.Error.WriteLine($"Signature field '{fieldName}' not found.");
                return;
            }

            // Create a PKCS#7 signature object using the certificate
            Signature signature = new PKCS7(certPath, certPass);

            // Sign the document using the signature field
            sigField.Sign(signature);

            // Lock the field to prevent further modifications
            sigField.ReadOnly = true;

            // Optional: enforce that any further changes must be incremental (append‑only)
            doc.Form.SignaturesAppendOnly = true;

            // Save the locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document saved with locked signature field: {outputPdf}");
    }
}
