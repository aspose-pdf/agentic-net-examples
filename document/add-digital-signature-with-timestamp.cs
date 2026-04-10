using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Security; // for DigestHashAlgorithm

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";

        // TSA server details (replace with real values)
        const string tsaServerUrl   = "https://tsa.example.com";
        const string tsaCredentials = "tsaUser:tsaPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page
            Page page = doc.Pages[1];
            // Define the rectangle where the signature appearance will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Create the signature field and add it to the document's form
            SignatureField signatureField = new SignatureField(page, rect);
            doc.Form.Add(signatureField);

            // Prepare the PKCS#7 signature object using the PFX certificate
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason   = "Document approved",
                    Location = "Head Office"
                };

                // Configure timestamp settings (trusted TSA)
                pkcs7.TimestampSettings = new TimestampSettings(
                    serverUrl:          tsaServerUrl,
                    basicAuthCredentials: tsaCredentials,
                    digestHashAlgorithm: DigestHashAlgorithm.Sha256);

                // Sign the document using the signature field
                signatureField.Sign(pkcs7);
            }

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}