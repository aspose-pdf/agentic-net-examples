using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page of the document
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.Name = "Signature1";
            sigField.PartialName = "Signature1";

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Load the self‑signed certificate and create a concrete PKCS7 object
            using (FileStream certStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(certStream, pfxPassword);
                // Sign the document using the created signature field
                sigField.Sign(pkcs7);
            }

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
