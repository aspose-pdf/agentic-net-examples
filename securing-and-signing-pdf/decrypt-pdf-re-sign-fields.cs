using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths and passwords – adjust as needed
        const string inputPdfPath  = "encrypted_input.pdf";
        const string outputPdfPath = "re_signed_output.pdf";
        const string userPassword  = "userPwd"; // password to open the encrypted PDF

        // Certificates to be used for re‑signing (one per signature field)
        // Ensure the arrays have at least as many entries as signature fields in the PDF.
        string[] certFiles  = { "cert1.pfx", "cert2.pfx", "cert3.pfx" };
        string[] certPasses = { "pwd1",      "pwd2",      "pwd3"      };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the encrypted document with the user password, then decrypt it.
        using (Document doc = new Document(inputPdfPath, userPassword))
        {
            // Decrypt the document – after this the PDF is no longer password‑protected.
            doc.Decrypt();

            // Gather all signature fields present in the document.
            var signatureFields = new System.Collections.Generic.List<SignatureField>();
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
                doc.Save(outputPdfPath);
                return;
            }

            // Iterate over each signature field and apply a new signature using a distinct certificate.
            for (int i = 0; i < signatureFields.Count; i++)
            {
                // Select certificate (cycle if fewer certificates than fields).
                int certIndex = i % certFiles.Length;
                string certPath = certFiles[certIndex];
                string certPass = certPasses[certIndex];

                if (!File.Exists(certPath))
                {
                    Console.Error.WriteLine($"Certificate file not found: {certPath}");
                    continue; // skip this field
                }

                // Create a concrete PKCS7 signature object (Signature is abstract).
                PKCS7 pkcs7 = new PKCS7(certPath, certPass)
                {
                    Reason      = $"Re‑signed by certificate {Path.GetFileNameWithoutExtension(certPath)}",
                    Location    = "Office",
                    ContactInfo = "contact@example.com"
                };

                // Sign the field with the prepared PKCS7 signature.
                signatureFields[i].Sign(pkcs7);
            }

            // Save the newly signed PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Document re‑signed and saved to '{outputPdfPath}'.");
    }
}
