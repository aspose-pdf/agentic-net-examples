using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // TimestampSettings lives here

class SignPdfWithTimestamp
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";
        const string tsaServerUrl   = "https://timestamp.example.com"; // trusted TSA URL
        const string licensePath    = "Aspose.Pdf.lic"; // optional – place your license file here

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // 1️⃣  Load Aspose.Pdf license (if present). This prevents the library
        //     from trying to start the external helper process (AsposePdfApi.exe)
        //     that is required only in trial mode.
        // ---------------------------------------------------------------------
        if (File.Exists(licensePath))
        {
            var license = new Aspose.Pdf.License();
            license.SetLicense(licensePath);
        }
        else
        {
            // When no license is supplied, disable the external PDF API to avoid
            // the "cannot find AsposePdfApi.exe" runtime error.
            Environment.SetEnvironmentVariable("Aspose.Pdf.DisablePdfApi", "true");
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 2️⃣  Create a signature field on the first page.
            //     Use the fully‑qualified Aspose.Pdf.Rectangle to avoid any
            //     ambiguity with Aspose.Pdf.Drawing.Rectangle.
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect);
            pdfDoc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // 3️⃣  Prepare the PKCS#7 signature using the PFX certificate.
            // -----------------------------------------------------------------
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword);

            // Configure timestamp settings (default hash algorithm is SHA‑256)
            pkcs7Signature.TimestampSettings = new TimestampSettings(
                serverUrl: tsaServerUrl,
                basicAuthCredentials: null,               // no basic auth; set "user:pass" if required
                digestHashAlgorithm: DigestHashAlgorithm.Sha256);

            // Optional: set additional signature properties
            pkcs7Signature.Reason   = "Document approved";
            pkcs7Signature.Location = "Company HQ";
            pkcs7Signature.ContactInfo = "contact@example.com";

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and timestamped successfully: {outputPdfPath}");
    }
}
