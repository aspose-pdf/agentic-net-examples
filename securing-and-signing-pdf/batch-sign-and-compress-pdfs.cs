using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be processed
        const string inputFolder = "InputPdfs";
        // Output folder for signed and compressed PDFs
        const string outputFolder = "SignedCompressedPdfs";
        // Path to the PFX certificate and its password
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Ensure the input directory exists – if it does not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now. Place PDFs to be processed in this folder and re‑run the program.");
            Directory.CreateDirectory(inputFolder);
            return; // nothing to process yet
        }

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_signed.pdf");

            try
            {
                // Load the PDF document (lifecycle rule: use Document constructor)
                using (Document doc = new Document(pdfPath))
                {
                    // Define the signature field rectangle (fully qualified to avoid ambiguity)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

                    // Create a signature field on the first page
                    SignatureField sigField = new SignatureField(doc.Pages[1], rect)
                    {
                        PartialName = "Signature"
                    };

                    // Initialize the concrete PKCS7 signature object from the PFX file
                    PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                    {
                        Reason = "Document approved",
                        Location = "Office",
                        ContactInfo = "contact@example.com"
                    };

                    // Apply the digital signature to the field
                    sigField.Sign(pkcs7);

                    // Compress the signed document (optimizes resources and reduces size)
                    doc.OptimizeResources();

                    // Save the signed and compressed PDF (lifecycle rule: use Document.Save)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
