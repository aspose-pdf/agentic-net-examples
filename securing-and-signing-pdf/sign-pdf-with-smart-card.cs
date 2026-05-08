using System;
using System.IO;
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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Open the personal (My) certificate store of the current user.
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection allCerts = store.Certificates;

            // Prompt the user to select a certificate from the smart card / store.
            // The OS will handle PIN entry when the private key is accessed.
            X509Certificate2Collection selected = X509Certificate2UI.SelectFromCollection(
                allCerts,
                "Select Certificate",
                "Choose the certificate stored on your smart card",
                X509SelectionFlag.SingleSelection);

            store.Close();

            if (selected == null || selected.Count == 0)
            {
                Console.Error.WriteLine("No certificate selected.");
                return;
            }

            X509Certificate2 cert = selected[0];

            // Create an ExternalSignature that works with certificates whose private key
            // is not exportable (e.g., smart cards). This will trigger the PIN prompt.
            ExternalSignature externalSignature = new ExternalSignature(cert);

            // Optional: set signature appearance properties
            externalSignature.Reason   = "Document approval";
            externalSignature.Location = Environment.MachineName;
            externalSignature.Date     = DateTime.Now;

            // Add a signature field to the first page (position can be adjusted as needed)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            doc.Pages[1].Annotations.Add(sigField);

            // Sign the document using the selected certificate
            sigField.Sign(externalSignature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
