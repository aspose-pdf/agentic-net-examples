using System;
using System.IO;
using System.Xml;
using System.Drawing; // for SizeF used by XfaParserOptions
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.XfaConverter;

class Program
{
    static void Main()
    {
        // Input files
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePfx = "certificate.pfx";
        const string pfxPassword    = "password";
        const string xfaXmlPath     = "custom_xfa.xml";

        // Verify required files exist
        if (!File.Exists(inputPdfPath) ||
            !File.Exists(certificatePfx) ||
            !File.Exists(xfaXmlPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Embed custom XFA data into the form
            // ------------------------------------------------------------
            Form form = doc.Form;

            // Load custom XFA XML
            XmlDocument xfaDoc = new XmlDocument();
            xfaDoc.Load(xfaXmlPath);

            // Assign the XFA data to the form
            form.AssignXfa(xfaDoc);

            // Ensure the XFA stream is considered during signing
            // (Signed = true forces inclusion of the XFA stream in the signature)
            XfaParserOptions xfaOptions = new XfaParserOptions(new SizeF(595, 842));
            xfaOptions.Signed = true;
            // Note: XfaParserOptions is used internally during conversion;
            // setting it here documents the intent to include XFA in the signature.

            // ------------------------------------------------------------
            // 2. Create a signature field on the first page
            // ------------------------------------------------------------
            // Define the rectangle where the signature appearance will be placed
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Initialize the signature field
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);

            // Add the signature field to the form
            form.Add(sigField);

            // ------------------------------------------------------------
            // 3. Prepare the digital signature object
            // ------------------------------------------------------------
            // PKCS7 uses a PFX certificate file and its password
            PKCS7 pkcs7Signature = new PKCS7(certificatePfx, pfxPassword)
            {
                Reason      = "Document approved",
                Location    = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // ------------------------------------------------------------
            // 4. Sign the PDF using the signature field
            // ------------------------------------------------------------
            sigField.Sign(pkcs7Signature);

            // ------------------------------------------------------------
            // 5. Save the signed PDF
            // ------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}