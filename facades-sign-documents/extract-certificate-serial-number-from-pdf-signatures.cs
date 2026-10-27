using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Linq;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains a digital signature
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Ensure the document has a form with fields
        if (pdfDocument?.Form?.Fields == null || !pdfDocument.Form.Fields.Any())
        {
            Console.WriteLine("No form fields (including signatures) found in the PDF.");
            return;
        }

        // Iterate over each form field and process signature fields
        foreach (Field field in pdfDocument.Form.Fields)
        {
            if (field is SignatureField signatureField)
            {
                // Extract the signing certificate from the signature field
                X509Certificate2 certificate = signatureField.ExtractCertificateObject();

                string fieldName = signatureField.PartialName ?? "<unknown>";

                if (certificate != null)
                {
                    // SerialNumber is a hexadecimal string representation of the certificate's serial number
                    string serialNumber = certificate.SerialNumber;
                    Console.WriteLine($"Signature field '{fieldName}' serial number: {serialNumber}");
                }
                else
                {
                    Console.WriteLine($"Signature field '{fieldName}' does not contain a certificate.");
                }
            }
        }
    }
}
