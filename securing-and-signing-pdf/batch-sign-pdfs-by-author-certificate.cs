using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs and folder for signed PDFs
        string inputFolder = "InputPdfs";
        string outputFolder = "SignedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Map a metadata value (e.g., Author) to a certificate file and its password
        var certificateMap = new Dictionary<string, (string pfxPath, string password)>(StringComparer.OrdinalIgnoreCase)
        {
            // Example mappings – adjust to your environment
            { "Alice", ("certs/alice.pfx", "alicePwd") },
            { "Bob",   ("certs/bob.pfx",   "bobPwd") }
        };

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(pdfFile))
                {
                    // Choose a certificate based on the document's Author metadata
                    string author = doc.Info.Author ?? string.Empty;
                    if (!certificateMap.TryGetValue(author, out var certInfo))
                    {
                        Console.WriteLine($"No certificate mapping for author '{author}'. Skipping {Path.GetFileName(pdfFile)}.");
                        continue;
                    }

                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                    {
                        Console.WriteLine($"Document has no pages: {pdfFile}");
                        continue;
                    }

                    // Create a signature field on the first page
                    Page page = doc.Pages[1];
                    // Rectangle defined as (llx, lly, urx, ury) in points
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
                    SignatureField signatureField = new SignatureField(page, rect)
                    {
                        Name = "Signature1"
                    };
                    page.Annotations.Add(signatureField);

                    // Initialise the concrete PKCS7 signature object with the selected certificate
                    PKCS7 pkcs7 = new PKCS7(certInfo.pfxPath, certInfo.password)
                    {
                        Reason = "Document approved",
                        Location = "Company HQ",
                        ContactInfo = "contact@example.com"
                    };

                    // Sign the field
                    signatureField.Sign(pkcs7);

                    // Save the signed PDF
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));
                    doc.Save(outputPath);
                    Console.WriteLine($"Signed PDF saved to {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfFile}: {ex.Message}");
            }
        }
    }
}
