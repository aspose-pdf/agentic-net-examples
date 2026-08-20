using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to be signed
        const string outputPdfPath  = "signed.pdf";     // Resulting signed PDF
        const string certificatePath = "certificate.pfx"; // PKCS#12 certificate file
        const string certificatePassword = "password";   // Certificate password

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF from a file stream
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        using (Document pdfDocument = new Document(pdfStream))
        {
            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create a signature field on the first page
            // Rectangle coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], sigRect)
            {
                PartialName = "Signature1",          // Field identifier
                // Optional visual properties can be set here (e.g., Background, Border)
            };

            // Add the signature field to the page annotations collection
            pdfDocument.Pages[1].Annotations.Add(signatureField);

            // Load the certificate (PFX) into a stream
            using (FileStream certStream = File.OpenRead(certificatePath))
            {
                // Create a PKCS7 detached signature object.
                // Using the constructor that accepts a stream for the appearance image is optional.
                // Here we use the parameterless constructor and set appearance properties manually if needed.
                PKCS7Detached pkcs7Signature = new PKCS7Detached();

                // Set signature metadata (optional but recommended)
                pkcs7Signature.Reason      = "I agree to the terms.";
                pkcs7Signature.ContactInfo = "contact@example.com";
                pkcs7Signature.Location    = "New York";
                pkcs7Signature.Date        = DateTime.UtcNow;

                // Sign the document using the signature field.
                // The overload that accepts a Signature object and a certificate stream is used.
                signatureField.Sign(pkcs7Signature, certStream, certificatePassword);
            }

            // Save the signed PDF to the output file
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
    }
}