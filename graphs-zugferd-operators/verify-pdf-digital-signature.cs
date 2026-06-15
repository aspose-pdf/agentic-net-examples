using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

namespace VerifySignatureExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a signature field (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                // Add a blank page
                Page page = sampleDoc.Pages.Add();

                // Define a rectangle for the signature field (coordinates are in points)
                Rectangle signatureRect = new Rectangle(100, 600, 200, 650);

                // Create the signature field and add it to the form on page 1
                SignatureField signatureField = new SignatureField(page, signatureRect);
                signatureField.PartialName = "Signature1";
                sampleDoc.Form.Add(signatureField, 1);

                // Save the PDF (no actual digital signature is applied – we only need a field to demonstrate verification)
                sampleDoc.Save("signed.pdf");
            }

            // Step 2: Open the PDF and enumerate its signature fields
            using (Document signedDoc = new Document("signed.pdf"))
            {
                // Aspose.PDF core library does not provide direct digital‑signature verification APIs.
                // Verification of a real digital signature would require the PdfFileSignature class from the Facades namespace,
                // which is prohibited by the task constraints. Therefore we limit the example to locating signature fields.

                Console.WriteLine("Enumerating signature fields in the document...");
                int signatureFieldCount = 0;
                foreach (Field field in signedDoc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        signatureFieldCount++;
                        Console.WriteLine($"Signature field name: {sigField.PartialName}");
                        // Output rectangle information for completeness
                        Rectangle rect = sigField.Rect;
                        Console.WriteLine($"  Position: LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}");
                    }
                }
                Console.WriteLine($"Number of signature fields found: {signatureFieldCount}");
            }
        }
    }
}
