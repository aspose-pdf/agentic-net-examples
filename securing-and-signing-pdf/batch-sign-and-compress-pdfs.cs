using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Optimization;

class Program
{
    // Adjust these paths as needed
    const string InputFolder   = @"C:\Pdf\Input";
    const string OutputFolder  = @"C:\Pdf\SignedCompressed";
    const string CertPath      = @"C:\Certificates\mycert.pfx";
    const string CertPassword  = "certPassword";

    static void Main()
    {
        // Ensure output directory exists
        Directory.CreateDirectory(OutputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(InputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfFile))
                {
                    // -------------------------------------------------
                    // Add a digital signature field on the first page
                    // -------------------------------------------------
                    // Define the rectangle where the signature will appear
                    Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

                    // Create the signature field and add it to the form
                    SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
                    doc.Form.Add(sigField, 1);

                    // Configure the signature properties
                    Signature signature = sigField.Signature; // read‑only property, but its members are writable
                    signature.ContactInfo = "contact@example.com";
                    signature.Location    = "Office";
                    signature.Reason      = "Document approval";
                    // Attach the certificate (PKCS#7) using the Forms API
                    PKCS1 pkcs = new PKCS1(CertPath, CertPassword);
                    sigField.Sign(pkcs);

                    // -------------------------------------------------
                    // Optimize (compress) the signed document
                    // -------------------------------------------------
                    OptimizationOptions optOptions = new OptimizationOptions
                    {
                        // Example: enable object stream compression
                        CompressObjects = true
                    };
                    doc.OptimizeResources(optOptions);

                    // -------------------------------------------------
                    // Save the processed PDF
                    // -------------------------------------------------
                    string outputPath = Path.Combine(OutputFolder, Path.GetFileName(pdfFile));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}