using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the output (decrypted & re‑signed) PDF.
        const string inputPdfPath = "encrypted_input.pdf";
        const string outputPdfPath = "re_signed_output.pdf";

        // Password required to open the encrypted PDF.
        const string pdfPassword = "userPassword";

        // Certificate files (PFX) and their passwords.
        // Each certificate will be used for one signature field.
        string[] certFiles = {
            "cert1.pfx",
            "cert2.pfx",
            "cert3.pfx"
        };
        string[] certPasswords = {
            "cert1Pass",
            "cert2Pass",
            "cert3Pass"
        };

        // Ensure the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Open the encrypted document.
        using (Document doc = new Document(inputPdfPath, pdfPassword))
        {
            // Decrypt the document (required before modifying signatures).
            doc.Decrypt();

            // Collect all signature fields from the form.
            var signatureFields = new System.Collections.Generic.List<SignatureField>();
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                        signatureFields.Add(sigField);
                }
            }

            if (signatureFields.Count == 0)
            {
                Console.WriteLine("No signature fields found in the document.");
                doc.Save(outputPdfPath); // Save the decrypted PDF unchanged.
                return;
            }

            // Sign each field with a different certificate.
            int certIndex = 0;
            foreach (SignatureField sigField in signatureFields)
            {
                if (certIndex >= certFiles.Length)
                {
                    Console.WriteLine("Not enough certificates for all signature fields.");
                    break;
                }

                // Create a PKCS7 signature object from the PFX file.
                PKCS7 pkcs7 = new PKCS7(certFiles[certIndex], certPasswords[certIndex]);
                // Optional: set appearance or metadata.
                // pkcs7.Reason = "Approved";
                // pkcs7.Location = "Office";
                // pkcs7.ContactInfo = "signer@example.com";

                // Apply the signature to the current field.
                sigField.Sign(pkcs7);

                certIndex++;
            }

            // Save the modified (decrypted and re‑signed) PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Document processed and saved to '{outputPdfPath}'.");
    }
}
