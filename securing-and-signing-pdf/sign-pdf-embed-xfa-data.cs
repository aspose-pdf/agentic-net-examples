using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "signed_output.pdf"; // signed PDF
        const string certPath       = "certificate.pfx";   // signing certificate
        const string certPassword   = "password";          // certificate password
        const string xfaDataPath    = "custom_xfa.xml";    // custom XFA data

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
        if (!File.Exists(xfaDataPath))
        {
            Console.Error.WriteLine($"XFA data file not found: {xfaDataPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Embed custom XFA form data into the document
            // ------------------------------------------------------------
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xfaDataPath);
            // Assign the XFA data to the form (core API)
            pdfDoc.Form.AssignXfa(xfaXml);

            // ------------------------------------------------------------
            // Create a signature field on the first page
            // ------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            // The constructor can take the Document and rectangle
            SignatureField sigField = new SignatureField(pdfDoc, sigRect)
            {
                PartialName = "Signature1", // field name
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray,
                Contents = "Signature"
            };
            // Add the signature field annotation to page 1 (1‑based indexing)
            pdfDoc.Pages[1].Annotations.Add(sigField);

            // ------------------------------------------------------------
            // Prepare the digital signature object
            // ------------------------------------------------------------
            // PKCS1 signature uses a PFX file and password
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPassword)
            {
                Reason      = "Document approved",
                ContactInfo = "contact@example.com",
                Location    = "Office"
                // Date defaults to current time; can be set explicitly if needed
            };

            // ------------------------------------------------------------
            // Sign the document using the created signature field
            // ------------------------------------------------------------
            sigField.Sign(pkcs1Signature);

            // ------------------------------------------------------------
            // Save the signed PDF
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
    }
}