using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // PDF to which the annotation will be added
        const string portfolioPdfPath  = "portfolio.pdf";      // PDF portfolio to embed
        const string outputPdfPath     = "output.pdf";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the target PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a rectangle for the annotation (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect)
            {
                // Optional: set a tooltip or contents text
                Contents = "Embedded PDF Portfolio"
            };

            // Embed the portfolio PDF as the rich media content
            using (FileStream portfolioStream = File.OpenRead(portfolioPdfPath))
            {
                // The first argument is a MIME type string; for a PDF use "application/pdf"
                richMedia.SetContent("application/pdf", portfolioStream);
            }

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(richMedia);

            // Disable printing by encrypting the document with permissions that omit PrintDocument
            Permissions perms = Permissions.ModifyContent |
                                 Permissions.ExtractContent |
                                 Permissions.ModifyTextAnnotations |
                                 Permissions.FillForm |
                                 Permissions.AssembleDocument |
                                 Permissions.PrintingQuality; // printing quality flag does NOT enable printing

            // Empty passwords mean no password is required to open the file,
            // but the permissions are still enforced.
            doc.Encrypt(userPassword: "", ownerPassword: "", permissions: perms, cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and printing disabled. Saved to '{outputPdfPath}'.");
    }
}