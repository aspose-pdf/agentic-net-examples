using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSignAndCompress
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputFolder   = @"C:\PdfInput";
        const string outputFolder  = @"C:\PdfSigned";
        const string certificatePath = @"C:\cert\mycert.pfx";
        const string certPassword    = "pfxPassword";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputFile);
            string outputFile = Path.Combine(outputFolder, fileName + "_signed.pdf");

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputFile))
            {
                // -------------------------------------------------
                // 1. Add a digital signature field to the first page
                // -------------------------------------------------
                // Define the signature appearance rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

                // Create the signature field and add it to the document form
                SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
                doc.Form.Add(sigField);

                // -------------------------------------------------
                // 2. Sign the field using the provided certificate
                // -------------------------------------------------
                // Use a concrete implementation of the abstract Signature class
                PKCS7 pkcs7 = new PKCS7(certificatePath, certPassword);
                pkcs7.Reason   = "Document approved";
                pkcs7.Location = "Company HQ";
                // Optional: pkcs7.ContactInfo = "contact@example.com";

                // Apply the signature to the field
                sigField.Sign(pkcs7);

                // -------------------------------------------------
                // 3. Compress the signed document
                // -------------------------------------------------
                // OptimizeResources removes unused objects and merges duplicates
                doc.OptimizeResources();

                // -------------------------------------------------
                // 4. Save the signed and compressed PDF
                // -------------------------------------------------
                doc.Save(outputFile);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputFile)} → {Path.GetFileName(outputFile)}");
        }

        Console.WriteLine("Batch signing and compression completed.");
    }
}
