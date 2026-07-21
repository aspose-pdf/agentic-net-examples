using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath      = "input.xml";          // Source XML file
        const string outputPdf    = "signed_output.pdf"; // Signed PDF output
        const string certPath     = "certificate.pfx";   // PFX certificate file
        const string certPassword = "pfxPassword";       // Certificate password

        // Verify source files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load XML and convert it to a PDF document
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            // Create a signature field on the first page
            // Rectangle coordinates: llx, lly, urx, ury (in points)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect);

            // Add the signature field to the document's form collection
            pdfDoc.Form.Add(sigField, 1);

            // Initialize the concrete PKCS7 signature object with the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
            pkcs7.Reason   = "Document approval";
            pkcs7.Location = "Office";

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
