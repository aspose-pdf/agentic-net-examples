using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define a rectangle for the second signature on page 3
            // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
            Rectangle signatureRect = new Rectangle(100, 500, 300, 550);

            // Create a signature field bound to the document and rectangle
            SignatureField signatureField = new SignatureField(doc, signatureRect);

            // Create a PKCS#1 digital signature using the provided certificate
            PKCS1 pkcs1Signature = new PKCS1(certificatePath, certificatePassword);

            // Apply the digital signature to the field
            signatureField.Sign(pkcs1Signature);

            // Add the signature field to page 3 of the document
            doc.Form.Add(signatureField, 3);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Second digital signature added on page 3 and saved to '{outputPath}'.");
    }
}
