using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";
        const string xfaDataPath    = "xfa_data.xml";

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
        using (Document doc = new Document(inputPdfPath))
        {
            // Load custom XFA data and assign it to the form
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xfaDataPath);
            doc.Form.AssignXfa(xfaXml);

            // Create a signature field on the first page
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                Name = "Signature1",
                PartialName = "Signature1"
            };
            doc.Pages[1].Annotations.Add(sigField);

            // Prepare the signature object (PKCS#7)
            Signature signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason   = "Document approval",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the signature field
            sigField.Sign(signature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}