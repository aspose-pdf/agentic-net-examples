using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Forms;

class BatchSigner
{
    static void Main()
    {
        // Input PDFs, output PDFs and the corresponding signature images.
        string[] inputPdfs  = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string[] outputPdfs = { "doc1_signed.pdf", "doc2_signed.pdf", "doc3_signed.pdf" };
        string[] sigImages  = { "sig1.png", "sig2.png", "sig3.png" };

        // Path to the digital certificate (PFX) and its password.
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        for (int i = 0; i < inputPdfs.Length; i++)
        {
            string inputPath  = inputPdfs[i];
            string outputPath = outputPdfs[i];
            string imagePath  = sigImages[i];

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                continue;
            }
            if (!File.Exists(imagePath))
            {
                Console.Error.WriteLine($"Signature image not found: {imagePath}");
                continue;
            }

            // Load the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // Place the signature on the last page.
                int pageNumber = doc.Pages.Count;

                // Rectangle (LLX, LLY, URX, URY) – points.
                // Fully qualify the Rectangle type to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle.
                var rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

                // Create a signature field.
                var sigField = new SignatureField(doc.Pages[pageNumber], rect)
                {
                    PartialName = $"Signature_{i}"
                };
                doc.Form.Add(sigField, pageNumber);

                // Prepare the PKCS#7 signature object.
                var pkcs7 = new PKCS7(certPath, certPassword)
                {
                    Reason      = "Approved",
                    Location    = "Company HQ",
                    ContactInfo = "contact@company.com"
                };

                // Sign the field – this writes the digital signature into the PDF.
                sigField.Sign(pkcs7);

                // Add a visible image on top of the signature field.
                var imageStamp = new ImageStamp(imagePath)
                {
                    Width      = rect.URX - rect.LLX,
                    Height     = rect.URY - rect.LLY,
                    LeftMargin = rect.LLX,
                    TopMargin  = rect.LLY,
                    Opacity    = 0.8f
                };
                doc.Pages[pageNumber].AddStamp(imageStamp);

                // Save the signed PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPath}' with image '{imagePath}'.");
        }
    }
}
