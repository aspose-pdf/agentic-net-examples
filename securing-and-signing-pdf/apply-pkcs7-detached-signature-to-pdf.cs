using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF file (will be read from a stream)
        const string inputPdfPath = "input.pdf";
        // Output signed PDF file
        const string outputPdfPath = "signed_output.pdf";
        // PKCS#7 certificate (PFX) file and its password
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document from a file stream
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        using (Document pdfDoc = new Document(pdfStream))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Define the rectangle where the visible signature will appear
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            Page firstPage = pdfDoc.Pages[1];
            SignatureField sigField = new SignatureField(firstPage, sigRect);
            // Optional: give the field a name (useful for later reference)
            sigField.PartialName = "Signature1";

            // Add the signature field to the document's form collection
            pdfDoc.Form.Add(sigField);

            // Create a PKCS#7 detached signature object using the certificate
            PKCS7Detached pkcs7Signature = new PKCS7Detached(certPath, certPassword);
            // Set optional signature properties
            pkcs7Signature.Reason = "Document approval";
            pkcs7Signature.ContactInfo = "signer@example.com";
            pkcs7Signature.Location = "New York";

            // Sign the field with the PKCS#7 detached signature
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF to the output file
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}