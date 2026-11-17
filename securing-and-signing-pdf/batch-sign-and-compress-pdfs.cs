using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Optimization;

class BatchSignAndCompress
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputFolder = @"C:\InputPdfs";
        const string outputFolder = @"C:\SignedCompressedPdfs";
        const string certificatePath = @"C:\certificates\mycert.pfx";
        const string certificatePwd = "certPassword";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFile);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfFile))
                {
                    // -------------------------------------------------
                    // Add a digital signature field on the first page
                    // -------------------------------------------------
                    // Define the rectangle where the signature will appear
                    Rectangle sigRect = new Rectangle(100, 100, 250, 150);

                    // Create the signature field on page 1
                    SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);

                    // Add the field to the document's form collection (page number = 1)
                    doc.Form.Add(sigField, 1);

                    // -------------------------------------------------
                    // Sign the field using a PKCS#7 certificate
                    // -------------------------------------------------
                    PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePwd);
                    sigField.Sign(pkcs7);

                    // -------------------------------------------------
                    // Compress the signed document to reduce size
                    // -------------------------------------------------
                    OptimizationOptions opt = OptimizationOptions.All();
                    opt.CompressImages = true;
                    opt.ImageQuality = 50; // adjust as needed
                    opt.RemoveUnusedObjects = true;
                    opt.RemoveUnusedStreams = true;
                    doc.OptimizeResources(opt);

                    // Save the signed and compressed PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
