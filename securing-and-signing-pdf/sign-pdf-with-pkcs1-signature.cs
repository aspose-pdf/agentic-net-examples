using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate (PFX) paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

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

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle for the signature field (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#1 signature object using the certificate file and password
            // This is the type expected by SignatureField.Sign()
            PKCS1 pkcs1Signature = new PKCS1(certificatePath, certificatePassword);

            // Sign the field with the PKCS#1 signature.
            // NOTE: The core Aspose.Pdf API does not expose a direct way to apply a
            // DocMDP certification (which would control post‑signing permissions).
            // The only supported way to create a certified PDF is via the
            // Aspose.Pdf.Facades.PdfFileSignature.Certify() method, which is outside the
            // allowed namespace set for this task. Therefore we sign the document
            // normally. If certification with specific permissions is required, the
            // Facades API must be used.
            sigField.Sign(pkcs1Signature);

            // Save the signed PDF (incremental update is performed automatically)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Document signed and saved to '{outputPdfPath}'.");
    }
}
