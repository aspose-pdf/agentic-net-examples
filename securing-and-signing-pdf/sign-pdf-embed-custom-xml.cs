using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Custom XML data to embed (example)
        const string customXml = "<SignatureData><Compliance>StandardXYZ</Compliance></SignatureData>";

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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ------------------------------------------------------------
                // 1. Add a signature field to the first page
                // ------------------------------------------------------------
                // Define the rectangle where the visible signature will appear
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                // Create the signature field and add it to the page
                SignatureField sigField = new SignatureField(pdfDoc, sigRect);
                pdfDoc.Pages[1].Annotations.Add(sigField);

                // ------------------------------------------------------------
                // 2. Create a PKCS#7 signature object using the certificate
                // ------------------------------------------------------------
                PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
                pkcs7Signature.Reason      = "Document approved";
                pkcs7Signature.ContactInfo = "contact@example.com";
                pkcs7Signature.Location    = "New York, USA";
                pkcs7Signature.Date        = DateTime.UtcNow;

                // ------------------------------------------------------------
                // 3. Sign the field with the PKCS#7 signature
                // ------------------------------------------------------------
                sigField.Sign(pkcs7Signature);

                // ------------------------------------------------------------
                // 4. Embed custom XML signature data (hidden on the page)
                // ------------------------------------------------------------
                // Place the XML as a hidden text fragment outside the visible page area
                TextFragment xmlFragment = new TextFragment(customXml);
                // Make the fragment effectively invisible
                xmlFragment.TextState.FontSize = 0.1f;
                xmlFragment.TextState.ForegroundColor = Aspose.Pdf.Color.White;
                // Position it far outside the page bounds
                xmlFragment.Position = new Position(-1000, -1000);
                pdfDoc.Pages[1].Paragraphs.Add(xmlFragment);

                // ------------------------------------------------------------
                // 5. Save the signed PDF
                // ------------------------------------------------------------
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}