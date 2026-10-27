using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate not found: {certificatePath}");
            return;
        }

        // Load PDF into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        {
            // Document must be disposed via using
            using (Document doc = new Document(pdfStream))
            {
                // Define the rectangle where the signature will appear (llx, lly, urx, ury)
                var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a signature field on the first page
                SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
                sigField.PartialName = "Signature1";
                // Add the field to the document's form collection (page numbers are 1‑based)
                doc.Form.Add(sigField, 1);

                // Prepare PKCS7 credentials
                PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document signed",
                    Location = "Office",
                    ContactInfo = "contact@example.com"
                };

                // Optional visual customisation
                pkcs7.CustomAppearance = new SignatureCustomAppearance
                {
                    ShowReason = true,
                    ShowLocation = true,
                    ShowContactInfo = true,
                    FontFamilyName = "Arial",
                    FontSize = 10,
                    BackgroundColor = Color.LightGray
                };

                // Sign the field
                sigField.Sign(pkcs7);

                // Save the signed PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
