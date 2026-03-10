using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            doc.Form.Add(sigField);

            // Retrieve a certificate with a private key from the smart card (CurrentUser\My store)
            X509Certificate2 certificate = null;
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                certificate = store.Certificates
                                   .Cast<X509Certificate2>()
                                   .FirstOrDefault(c => c.HasPrivateKey);
                store.Close();
            }

            if (certificate == null)
            {
                Console.Error.WriteLine("No certificate with a private key was found on the smart card.");
                return;
            }

            // Create an external signature object that uses the smart‑card certificate
            ExternalSignature externalSignature = new ExternalSignature(certificate)
            {
                Reason   = "Document approval",
                Location = "Office",
                Date     = DateTime.Now
            };

            // Apply the digital signature to the signature field
            sigField.Sign(externalSignature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}