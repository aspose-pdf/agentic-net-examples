using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input encrypted PDF and its password
        const string inputPdfPath = "encrypted_input.pdf";
        const string pdfPassword = "userPassword";

        // Output path for the newly signed PDF
        const string outputPdfPath = "re_signed_output.pdf";

        // Certificate files (PFX) and their passwords – one per signature field
        string[] certFiles = { "cert1.pfx", "cert2.pfx", "cert3.pfx" };
        string[] certPasses = { "pass1", "pass2", "pass3" };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Verify that certificate files exist
        for (int i = 0; i < certFiles.Length; i++)
        {
            if (!File.Exists(certFiles[i]))
            {
                Console.Error.WriteLine($"Certificate file not found: {certFiles[i]}");
                return;
            }
        }

        try
        {
            // Load the encrypted PDF using the user password
            using (Document doc = new Document(inputPdfPath, pdfPassword))
            {
                // Decrypt the document (removes encryption)
                doc.Decrypt();

                int certIndex = 0; // rotate through certificates

                // Iterate over all pages
                foreach (Page page in doc.Pages)
                {
                    // Annotations collection uses 1‑based indexing
                    for (int i = 1; i <= page.Annotations.Count; i++)
                    {
                        Annotation ann = page.Annotations[i];

                        // Look for signature fields
                        if (ann is SignatureField sigField)
                        {
                            // Create a concrete PKCS7 signature object from the current certificate
                            PKCS7 pkcs7 = new PKCS7(certFiles[certIndex], certPasses[certIndex]);
                            // Optional: set additional properties (Reason, Location, ContactInfo) if needed
                            // pkcs7.Reason = "Re‑signed";

                            // Sign (or re‑sign) the field with this certificate
                            sigField.Sign(pkcs7);

                            // Move to the next certificate (cycle if needed)
                            certIndex = (certIndex + 1) % certFiles.Length;
                        }
                    }
                }

                // Save the fully signed PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Document decrypted and re‑signed successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
