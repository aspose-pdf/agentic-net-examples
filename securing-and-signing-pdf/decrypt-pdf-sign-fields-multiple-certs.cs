using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "signed.pdf";
        const string userPassword = "userpass";

        // Paths to the certificates and their passwords.
        // Add as many as needed; the code will cycle through them.
        string[] certFiles = { "cert1.pfx", "cert2.pfx", "cert3.pfx" };
        string[] certPasswords = { "pass1", "pass2", "pass3" };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF with the user password.
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Decrypt the document so that modifications can be made.
            doc.Decrypt();

            // Ensure the document contains a form.
            if (doc.Form == null)
            {
                Console.WriteLine("Document does not contain any form fields.");
                doc.Save(outputPath);
                return;
            }

            // Gather all signature fields.
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
                doc.Save(outputPath);
                return;
            }

            // Sign each field with a different certificate (cycle if needed).
            int certIndex = 0;
            foreach (SignatureField sigField in signatureFields)
            {
                string certPath = certFiles[certIndex % certFiles.Length];
                string certPass = certPasswords[certIndex % certPasswords.Length];

                if (!File.Exists(certPath))
                {
                    Console.Error.WriteLine($"Certificate file not found: {certPath}");
                    certIndex++;
                    continue; // Skip this field.
                }

                // Create a PKCS7 signature object from the certificate.
                var pkcs7 = new PKCS7(certPath, certPass)
                {
                    Reason = $"Signed with certificate #{certIndex + 1}",
                    ContactInfo = "contact@example.com",
                    Location = "Location"
                };

                // Apply the signature to the current field.
                sigField.Sign(pkcs7);

                certIndex++;
            }

            // Save the newly signed PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
