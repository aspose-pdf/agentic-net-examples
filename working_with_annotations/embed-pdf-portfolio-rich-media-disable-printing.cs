using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";      // source PDF
        const string portfolioPdf  = "portfolio.pdf";  // PDF portfolio to embed
        const string outputPdf     = "output.pdf";     // result PDF
        const string userPassword  = "";               // empty user password
        const string ownerPassword = "owner123";       // owner password for encryption

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(portfolioPdf))
        {
            Console.Error.WriteLine("Required input files are missing.");
            return;
        }

        // Load the source document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // Embed the PDF portfolio as the rich‑media content
            using (FileStream fs = File.OpenRead(portfolioPdf))
            {
                // First argument is a name for the embedded file
                richMedia.SetContent("portfolio.pdf", fs);
            }

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(richMedia);

            // Set permissions that exclude printing
            Permissions perms = Permissions.ModifyContent |
                                 Permissions.ExtractContent |
                                 Permissions.ModifyTextAnnotations |
                                 Permissions.FillForm |
                                 Permissions.AssembleDocument |
                                 Permissions.PrintingQuality; // PrintDocument flag is omitted

            // Encrypt the document to enforce the permissions (no user password)
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPdf}'.");
    }
}