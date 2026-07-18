using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF and its user password
        const string inputPdf = "encrypted.pdf";
        const string userPassword = "userpwd";

        // Define the certificates (PFX file + password) that will be used for re‑signing.
        // Each signature field will be signed with the next certificate in the list (cycling if needed).
        var certificates = new List<(string Path, string Password)>
        {
            ("cert1.pfx", "pwd1"),
            ("cert2.pfx", "pwd2"),
            ("cert3.pfx", "pwd3")
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Open the encrypted PDF using the user password.
        using (Document doc = new Document(inputPdf, userPassword))
        {
            // Decrypt the document so that further modifications are allowed.
            doc.Decrypt();

            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("The document does not contain a form.");
                return;
            }

            // Retrieve all signature fields present in the document.
            var signatureFields = new List<SignatureField>();
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    signatureFields.Add(sigField);
                }
            }

            if (signatureFields.Count == 0)
            {
                Console.WriteLine("No signature fields found in the document.");
            }
            else
            {
                int certIndex = 0; // Index to pick certificates cyclically.

                foreach (SignatureField sigField in signatureFields)
                {
                    // Select the certificate for this field.
                    var certInfo = certificates[certIndex % certificates.Count];

                    // Create a PKCS7 signature object from the PFX file and its password.
                    PKCS7 pkcs7 = new PKCS7(certInfo.Path, certInfo.Password);

                    // Optional: set additional signature properties.
                    // pkcs7.Reason = "Document re‑signed";
                    // pkcs7.Location = "Automated Process";
                    // pkcs7.ContactInfo = "no-reply@example.com";

                    // Sign the field with the created PKCS7 signature.
                    sigField.Sign(pkcs7);

                    Console.WriteLine($"Signed field '{sigField.PartialName}' using certificate '{Path.GetFileName(certInfo.Path)}'.");

                    certIndex++;
                }
            }

            // Save the newly signed PDF.
            const string outputPdf = "re_signed.pdf";
            doc.Save(outputPdf);
            Console.WriteLine($"Re‑signed PDF saved to '{outputPdf}'.");
        }
    }
}
