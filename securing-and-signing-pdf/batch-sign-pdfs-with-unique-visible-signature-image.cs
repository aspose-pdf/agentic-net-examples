using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations; // Added to resolve Border type

class BatchSigner
{
    // Adjust these paths as needed
    private const string InputFolder = @"C:\PdfBatch\Input";
    private const string OutputFolder = @"C:\PdfBatch\Signed";
    private const string CertificatePath = @"C:\Certificates\signer.pfx";
    private const string CertificatePassword = "pfxPassword";

    static void Main()
    {
        // Ensure output directory exists
        Directory.CreateDirectory(OutputFolder);

        // Enumerate all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(InputFolder, "*.pdf"))
        {
            // Derive a unique image for this document (e.g., same name with .png)
            string imagePath = System.IO.Path.ChangeExtension(pdfPath, ".png");

            // Output file name
            string outputPath = System.IO.Path.Combine(OutputFolder, System.IO.Path.GetFileName(pdfPath));

            // Process each document
            SignPdf(pdfPath, imagePath, outputPath);
        }

        Console.WriteLine("Batch signing completed.");
    }

    private static void SignPdf(string pdfPath, string imagePath, string outputPath)
    {
        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                throw new InvalidOperationException("Document contains no pages.");

            // Define the rectangle where the signature field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // Optional visual tweaks
                Color = Aspose.Pdf.Color.LightGray
            };
            // Border must be set after the field instance exists (requires parent annotation)
            sigField.Border = new Border(sigField) { Width = 1 };

            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS1 signature object with the certificate
            Signature signature = new PKCS1(CertificatePath, CertificatePassword);

            // If a unique image exists for this document, use it as the visible appearance
            if (File.Exists(imagePath))
            {
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // Create a temporary PKCS1 instance that contains the custom appearance
                    PKCS1 appearanceOnly = new PKCS1(imgStream);
                    // Transfer the custom appearance to the main signature object
                    signature.CustomAppearance = appearanceOnly.CustomAppearance;
                }
            }

            // Sign the field with the prepared signature object
            sigField.Sign(signature);

            // Save the signed PDF
            doc.Save(outputPath);
        }
    }
}
