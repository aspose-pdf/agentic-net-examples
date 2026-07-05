using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSigner
{
    static void Main()
    {
        // Folder containing PDFs to be signed
        string inputFolder = "InputPdfs";
        // Folder where signed PDFs will be written
        string outputFolder = "SignedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Map a metadata value (e.g., Title) to a certificate file and its password
        var certMap = new Dictionary<string, (string certPath, string password)>
        {
            { "Finance", ("certs/finance.pfx", "finPass") },
            { "Legal",   ("certs/legal.pfx",   "legPass") },
            // Fallback certificate
            { "Default", ("certs/default.pfx", "defPass") }
        };

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (1‑based page indexing)
                using (Document doc = new Document(pdfPath))
                {
                    // Example: use the document Title as the selector; fallback to "Default"
                    string meta = doc.Info.Title ?? "Default";

                    if (!certMap.TryGetValue(meta, out var certInfo))
                    {
                        certInfo = certMap["Default"];
                    }

                    // Create a concrete PKCS7 signature object from the selected certificate
                    PKCS7 signature = new PKCS7(certInfo.certPath, certInfo.password)
                    {
                        Reason = "Batch signing",
                        Location = Environment.MachineName,
                        ContactInfo = "admin@example.com"
                    };

                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                    {
                        Console.Error.WriteLine($"No pages in {pdfPath}");
                        continue;
                    }

                    // Place the signature on the first page, bottom‑right corner
                    Page page = doc.Pages[1]; // 1‑based indexing

                    // Define the rectangle for the signature field (llx, lly, urx, ury)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(400, 50, 550, 150);

                    // Create the signature field and add it to the page annotations
                    SignatureField sigField = new SignatureField(doc, rect);
                    page.Annotations.Add(sigField);

                    // Sign the document using the created field
                    sigField.Sign(signature);

                    // Save the signed PDF to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Signed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to sign '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }
    }
}
