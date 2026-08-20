using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF containing the empty signature field
        const string outputPdfPath = "signed_locked.pdf";  // Resulting PDF
        const string pfxPath       = "certificate.pfx";   // Signing certificate
        const string pfxPassword   = "password";          // Certificate password
        const string fieldName     = "ClientSignature";   // Name of the signature field to lock

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Retrieve the signature field by name
                SignatureField sigField = doc.Form[fieldName] as SignatureField;
                if (sigField == null)
                {
                    Console.Error.WriteLine($"Signature field '{fieldName}' not found.");
                    return;
                }

                // Create a PKCS#7 signature object using the certificate
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason   = "Approved by client",
                    Location = "Client Office",
                    ContactInfo = "client@example.com",
                    Date = DateTime.UtcNow
                };

                // Sign the field
                sigField.Sign(pkcs7);

                // Lock the field to prevent further changes
                sigField.ReadOnly = true;

                // Ensure that any further modifications are saved as incremental updates only
                doc.Form.SignaturesAppendOnly = true;

                // Save the signed and locked PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Document signed and field '{fieldName}' locked successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}