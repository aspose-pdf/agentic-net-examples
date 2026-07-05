using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // PDF to receive the annotation
        const string portfolioPdfPath  = "portfolio.pdf";      // PDF portfolio to embed
        const string outputPdfPath     = "output.pdf";
        const string userPassword      = "user123";
        const string ownerPassword     = "owner123";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPdfPath}");
            return;
        }

        // Load the target document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a rectangle for the annotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // Embed the portfolio PDF as the rich media content
            using (FileStream portfolioStream = File.OpenRead(portfolioPdfPath))
            {
                // MIME type for PDF is "application/pdf"
                richMedia.SetContent("application/pdf", portfolioStream);
            }

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(richMedia);

            // Disable printing by encrypting the document without the PrintDocument permission
            Permissions perms = Permissions.ModifyContent |
                                 Permissions.ExtractContent |
                                 Permissions.ModifyTextAnnotations |
                                 Permissions.FillForm |
                                 Permissions.AssembleDocument |
                                 Permissions.PrintingQuality; // exclude PrintDocument

            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and document saved to '{outputPdfPath}'.");
    }
}