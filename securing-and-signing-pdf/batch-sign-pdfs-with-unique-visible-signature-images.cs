using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSigner
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder containing signature images (one per PDF, same base name, e.g., doc1.png)
        const string signatureImagesFolder = "SignatureImages";
        // Folder where signed PDFs will be written
        const string outputFolder = "SignedPdfs";

        // Ensure the required folders exist before processing
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist. Create the folder and place PDF files inside.");
            return;
        }
        if (!Directory.Exists(signatureImagesFolder))
        {
            Console.Error.WriteLine($"Signature images folder '{signatureImagesFolder}' does not exist. Create the folder and place PNG files inside.");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string imagePath = Path.Combine(signatureImagesFolder, baseName + ".png");

            if (!File.Exists(imagePath))
            {
                Console.Error.WriteLine($"Signature image not found for '{baseName}'. Skipping.");
                continue;
            }

            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Choose the page where the signature will appear (first page in this example)
                Page page = doc.Pages[1];

                // Define the rectangle for the visible signature field (coordinates in points)
                // Aspose.Pdf.Rectangle expects (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

                // Create a signature field on the selected page
                SignatureField sigField = new SignatureField(page, rect);
                page.Annotations.Add(sigField);

                // Load the image that will be used as the visible signature appearance
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // PKCS1 constructor that accepts an image stream defines the appearance of the signature.
                    // No certificate is required for a visual‑only signature.
                    PKCS1 pkcs1 = new PKCS1(imgStream)
                    {
                        Reason = "Approved",
                        Location = "Company HQ"
                    };

                    // Apply the digital signature to the field
                    sigField.Sign(pkcs1);
                }

                // Save the signed PDF to the output folder
                string outputPath = Path.Combine(outputFolder, baseName + "_signed.pdf");
                doc.Save(outputPath);
                Console.WriteLine($"Signed PDF saved: {outputPath}");
            }
        }
    }
}
