using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSignAndCompress
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Folder where signed & compressed PDFs will be saved
        const string outputFolder = @"C:\SignedPdfs";

        // Path to the PFX certificate and its password
        const string pfxPath = @"C:\Certificates\mycert.pfx";
        const string pfxPassword = "certPassword";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFile);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfFile))
            {
                // Create a visible signature field on the first page
                // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Add the signature field to page 1
                SignatureField signatureField = new SignatureField(doc.Pages[1], rect);

                // Initialize the digital signature using the PFX file (concrete PKCS7 class)
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason = "Document approved",
                    Location = "Company HQ",
                    ContactInfo = "contact@example.com",
                    // The Date property is optional; if needed, set it explicitly
                    // Date = DateTime.UtcNow // PKCS7 uses the current time by default
                };

                // Apply the signature to the field
                signatureField.Sign(pkcs7);

                // Compress the signed document by optimizing its resources
                doc.OptimizeResources();

                // Save the signed and compressed PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }

        Console.WriteLine("Batch signing and compression completed.");
    }
}
