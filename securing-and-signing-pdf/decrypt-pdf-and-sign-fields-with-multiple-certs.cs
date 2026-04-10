using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths and passwords – adjust as needed
        const string inputPdf = "encrypted.pdf";
        const string outputPdf = "signed.pdf";
        const string userPassword = "userpwd";

        // Example certificate files and their passwords
        string[] certFiles = { "cert1.pfx", "cert2.pfx", "cert3.pfx" };
        string[] certPasswords = { "pwd1", "pwd2", "pwd3" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the encrypted PDF using the user password
        using (Document doc = new Document(inputPdf, userPassword))
        {
            // Decrypt the document – after this the PDF is in clear text
            doc.Decrypt();

            // Collect all signature fields present in the form
            List<SignatureField> signatureFields = new List<SignatureField>();
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    signatureFields.Add(sigField);
                }
            }

            // Sign each signature field with a different certificate
            for (int i = 0; i < signatureFields.Count; i++)
            {
                // Choose a certificate (cycle if there are fewer certificates than fields)
                int certIndex = i % certFiles.Length;
                string certPath = certFiles[certIndex];
                string certPwd = certPasswords[certIndex];

                if (!File.Exists(certPath))
                {
                    Console.Error.WriteLine($"Certificate file not found: {certPath}");
                    continue; // skip this field if the certificate is missing
                }

                // Create a concrete PKCS7 signature object from the certificate file
                PKCS7 signature = new PKCS7(certPath, certPwd);
                // Optional: set additional signature properties
                signature.Reason = $"Signed with certificate #{certIndex + 1}";
                signature.Location = "Automated Signing";

                // Apply the signature to the field
                signatureFields[i].Sign(signature);
            }

            // Save the newly signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
