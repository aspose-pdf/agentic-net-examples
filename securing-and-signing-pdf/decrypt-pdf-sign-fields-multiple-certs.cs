using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // SignatureField, PKCS7

class Program
{
    static void Main()
    {
        // Paths and passwords (adjust as necessary)
        const string inputPdfPath = "encrypted_input.pdf";
        const string outputPdfPath = "re_signed_output.pdf";
        const string pdfUserPassword = "userPassword"; // password to open the encrypted PDF

        // Folder containing certificates (PFX files) and their password
        const string certFolder = "certs";
        const string certPassword = "certPassword";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!Directory.Exists(certFolder))
        {
            Console.Error.WriteLine($"Certificate folder not found: {certFolder}");
            return;
        }

        // Load all certificate files from the folder
        string[] certFiles = Directory.GetFiles(certFolder, "*.pfx");
        if (certFiles.Length == 0)
        {
            Console.Error.WriteLine("No certificate files (*.pfx) found in the certificate folder.");
            return;
        }

        try
        {
            // Open the encrypted PDF with the user password and decrypt it
            using (Document pdfDoc = new Document(inputPdfPath, pdfUserPassword))
            {
                pdfDoc.Decrypt(); // Decrypt the document (no parameters)

                // Retrieve all signature fields in the document
                var signatureFields = pdfDoc.Form?.Fields?.OfType<SignatureField>();
                if (signatureFields == null || !signatureFields.Any())
                {
                    Console.WriteLine("No signature fields found in the PDF.");
                }
                else
                {
                    int index = 0;
                    foreach (SignatureField sigField in signatureFields)
                    {
                        // Select a certificate (cycle if fewer certificates than fields)
                        string certPath = certFiles[index % certFiles.Length];

                        // Create PKCS7 object and set metadata
                        PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                        {
                            Reason = "Re‑signed by automated process",
                            Location = "Automated System",
                            ContactInfo = "no-reply@example.com"
                        };

                        // Sign the field with the PKCS7 object
                        sigField.Sign(pkcs7);

                        Console.WriteLine($"Signature field '{sigField.PartialName}' re‑signed with certificate '{Path.GetFileName(certPath)}'.");
                        index++;
                    }
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Re‑signed PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Incorrect PDF password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
