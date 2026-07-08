using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the signature is added as an incremental update to keep layout unchanged
            doc.Form.SignaturesAppendOnly = true;

            // Create an invisible signature field (zero‑size rectangle)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            SignatureField sigField = new SignatureField(doc, rect);
            // Remove any visual cues
            sigField.Border = null;
            sigField.Color = null;

            // Add the signature field to the first page
            doc.Pages[1].Annotations.Add(sigField);

            // Prepare a PKCS#7 signature object and set the signing time
            PKCS7 pkcs7 = new PKCS7
            {
                Date = DateTime.Now // records signing time
            };

            // Load the certificate (PFX) and sign the field
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                sigField.Sign(pkcs7, pfxStream, pfxPassword);
            }

            // Save the signed PDF (incremental update preserves original layout)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}