using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- XMP metadata (native) -----
                // Standard properties
                doc.Metadata["Title"] = "Signed Document";
                doc.Metadata["CreatorTool"] = "Aspose.Pdf for .NET";
                // Custom property describing the signing process
                doc.Metadata["SigningProcess"] = "Document signed with digital certificate using Aspose.Pdf";

                // ----- Digital signature -----
                string reason = "Document approved";
                string location = "Company HQ";

                // Create a signature field on the first page
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
                SignatureField signatureField = new SignatureField(firstPage, sigRect)
                {
                    PartialName = "Signature1"
                };
                // Add the signature field to the document's form (AcroForm)
                doc.Form.Add(signatureField, 1);

                // Create a concrete PKCS#7 digital signature using the certificate
                PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                {
                    Reason = reason,
                    Location = location
                };

                // Apply the signature to the field
                signatureField.Sign(pkcs7);

                // Save the signed PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF signed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
