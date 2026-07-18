using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_invisible.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure that later saves use incremental updates so existing layout stays unchanged
            doc.Form.SignaturesAppendOnly = true;

            // Create an invisible signature field on the first page.
            // Rectangle with zero width/height placed at the origin makes the field non‑visible.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            sigField.PartialName = "InvisibleSignature";

            // Initialize a PKCS#7 signature object with the certificate.
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            // Record the signing time.
            pkcs7.Date = DateTime.Now;
            // Hide the signature appearance (no visible properties will be shown).
            pkcs7.ShowProperties = false;

            // Apply the signature to the field.
            sigField.Sign(pkcs7);

            // Save the signed PDF. No SaveOptions are needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}