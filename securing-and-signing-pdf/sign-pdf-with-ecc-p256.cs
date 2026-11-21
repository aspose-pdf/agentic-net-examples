using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed.pdf";
        const string certPath      = "ecc_cert.pfx";
        const string certPassword  = "password";

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

        // Load the source PDF into an expandable memory stream.
        byte[] pdfBytes = File.ReadAllBytes(inputPdfPath);
        using var sourceStream = new MemoryStream();
        sourceStream.Write(pdfBytes, 0, pdfBytes.Length);
        sourceStream.Position = 0;

        // Open the document from the expandable stream.
        using var pdfDoc = new Document(sourceStream);

        // Create a signature field on the first page (adjust rectangle as needed).
        // Use the Aspose.Pdf.Rectangle type for annotation bounds.
        var rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200); // lower‑left X,Y, upper‑right X,Y
        var signatureField = new SignatureField(pdfDoc.Pages[1], rect)
        {
            PartialName = "ECCSignature"
        };
        pdfDoc.Form.Add(signatureField, 1);

        // Prepare PKCS#7 signature using the ECC P‑256 certificate.
        var pkcs7 = new PKCS7(certPath, certPassword)
        {
            Reason      = "Signed with ECC P‑256",
            Location    = "My Office",
            ContactInfo = "john.doe@example.com"
        };

        // Sign the field. The signature is written directly into the source stream.
        signatureField.Sign(pkcs7);

        // After signing, the source stream now contains the signed PDF.
        File.WriteAllBytes(outputPdfPath, sourceStream.ToArray());

        Console.WriteLine($"PDF signed successfully: {outputPdfPath}");
    }
}
