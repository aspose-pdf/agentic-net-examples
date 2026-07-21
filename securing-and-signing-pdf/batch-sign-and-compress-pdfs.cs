using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSignAndCompress
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where signed & compressed PDFs will be saved
        const string outputFolder = "SignedCompressed";
        // Path to the PFX certificate and its password
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Add a signature field on the first page (coordinates: llx, lly, urx, ury)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
                    SignatureField sigField = new SignatureField(doc.Pages[1], rect)
                    {
                        PartialName = "Signature1"
                    };
                    doc.Form.Add(sigField);

                    // Create a concrete PKCS7 signature object from the PFX file
                    PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                    {
                        Reason = "Document approved",
                        Location = "Company HQ"
                    };

                    // Apply the digital signature to the field
                    sigField.Sign(pkcs7);

                    // Compress the signed PDF by optimizing resources
                    doc.OptimizeResources();

                    // Save the signed and compressed PDF (PDF format, no extra SaveOptions needed)
                    doc.Save(outPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
