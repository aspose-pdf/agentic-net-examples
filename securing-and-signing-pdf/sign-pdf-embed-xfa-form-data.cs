using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to required files
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string xfaDataPath    = "custom_xfa.xml";

        // Verify that all files exist before proceeding
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
        if (!File.Exists(xfaDataPath))
        {
            Console.Error.WriteLine($"XFA data file not found: {xfaDataPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load custom XFA XML and assign it to the document's form
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xfaDataPath);
            // The Form property provides access to the AcroForm; AssignXfa replaces the XFA data
            pdfDoc.Form.AssignXfa(xfaXml);

            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(pdfDoc, sigRect)
            {
                PartialName = "Signature1" // field name
            };
            // Add the signature field annotation to the page
            pdfDoc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason      = "Document signed for business purposes",
                ContactInfo = "contact@example.com",
                Location    = "Head Office"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
    }
}