using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class BatchSignAndCompress
{
    static void Main()
    {
        // Build platform‑independent input / output folders relative to the executable location
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = System.IO.Path.Combine(baseDir, "Pdf", "Input");
        string outputFolder = System.IO.Path.Combine(baseDir, "Pdf", "Output");

        // Digital certificate (PFX) and its password – also built relative to the executable
        string pfxPath = System.IO.Path.Combine(baseDir, "Cert", "mycert.pfx");
        const string pfxPassword = "myPassword";

        // Ensure folders exist (creates them if they are missing)
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine output file path (fully qualify System.IO.Path to avoid ambiguity)
            string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfPath));

            // Load the PDF document (using the recommended constructor)
            using (Document doc = new Document(pdfPath))
            {
                // Add a signature field on the first page
                // Fully qualified Rectangle avoids ambiguity with Aspose.Pdf.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                Page firstPage = doc.Pages[1]; // 1‑based indexing
                SignatureField sigField = new SignatureField(firstPage, rect)
                {
                    PartialName = "Signature1"
                };

                // Create a concrete Signature object (PKCS7) from the PFX file
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
                // Optionally set additional properties
                // pkcs7.Reason = "Approved";
                // pkcs7.Location = "Head Office";
                // pkcs7.ContactInfo = "admin@example.com";

                // Apply the signature to the field
                sigField.Sign(pkcs7);

                // Optimize resources to compress the signed document
                doc.OptimizeResources();

                // Save the signed and compressed PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {System.IO.Path.GetFileName(pdfPath)} → {outputPath}");
        }
    }
}