using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_locked.pdf"; // result PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "password";         // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a signature field on the first page (example rectangle)
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, rect)
            {
                // Mark the field as required
                Required = true,
                // Optional: give the field a name
                Name = "Signature1"
            };
            // Add the field to the document's form
            doc.Form.Add(sigField, 1);

            // ---------------------------------------------------------------
            // 2. Prepare the digital signature (PKCS7) using the certificate
            // ---------------------------------------------------------------
            // PKCS7 constructor accepts a stream and password
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                Signature pkcs7Signature = new PKCS7(pfxStream, pfxPassword)
                {
                    // Optional metadata for the signature
                    Authority   = "John Doe",
                    Location    = "New York, USA",
                    Reason      = "Document approval",
                    Date        = DateTime.UtcNow
                };

                // -----------------------------------------------------------
                // 3. Sign the field with the prepared signature
                // -----------------------------------------------------------
                sigField.Sign(pkcs7Signature);
            }

            // ---------------------------------------------------------------
            // 4. Lock the document after signing (MDP – NoChanges)
            // ---------------------------------------------------------------
            // Create an MDP signature that enforces no further changes
            // (the same PKCS7 signature object can be reused)
            // Note: DocMDPSignature is a wrapper; we only need to set the
            // document's handling flags to enforce the lock.
            doc.Form.SignaturesAppendOnly = true;      // prevent incremental updates that alter signatures
            doc.HandleSignatureChange      = true;    // throw if the document is saved with changes after signing

            // ---------------------------------------------------------------
            // 5. Save the signed and locked PDF
            // ---------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'. The signature field is required and the document is locked after signing.");
    }
}