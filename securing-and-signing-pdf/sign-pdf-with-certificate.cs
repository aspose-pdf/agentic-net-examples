using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // NOTE:
        // The original example retrieved a PFX certificate from Azure Key Vault
        // using the Azure.Identity and Azure.Security.KeyVault.Secrets packages.
        // Those packages are not referenced in the project, which caused the
        // CS0246 errors ("The type or namespace name 'Azure' could not be found").
        // ---------------------------------------------------------------------
        // To make the sample compile without adding external NuGet packages we
        // replace the Azure Key Vault call with a simple local‑file load.  In a
        // real application you should restore the Azure SDK packages and use
        // the original code – the rest of the signing logic remains unchanged.
        // ---------------------------------------------------------------------

        // Path to a PFX file that contains the signing certificate (including the
        // private key).  Replace this with the appropriate path or re‑introduce the
        // Azure Key Vault retrieval logic once the required packages are added.
        const string pfxFilePath = "certificate.pfx"; // <-- update as needed
        const string pfxPassword = ""; // empty if the PFX has no password

        // PDF files
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        // Load the PFX certificate into a memory stream
        if (!File.Exists(pfxFilePath))
        {
            Console.WriteLine($"PFX file not found: {pfxFilePath}");
            return;
        }
        byte[] pfxBytes = File.ReadAllBytes(pfxFilePath);
        using var pfxStream = new MemoryStream(pfxBytes);

        // Load the PDF document
        using (var document = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (adjust rectangle as needed)
            var rect = new Rectangle(100, 100, 200, 150);
            var signatureField = new SignatureField(document.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            document.Form.Add(signatureField, 1);

            // Create a PKCS#1 signature object using the PFX stream
            var pkcs1Signature = new PKCS1(pfxStream, pfxPassword);

            // Optional: set signature appearance and metadata
            pkcs1Signature.Reason = "Document approved";
            pkcs1Signature.ContactInfo = "contact@example.com";
            pkcs1Signature.Location = "Office";

            // Sign the field
            signatureField.Sign(pkcs1Signature);

            // Save the signed PDF
            document.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
    }
}
