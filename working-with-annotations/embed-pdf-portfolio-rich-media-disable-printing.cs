using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string outputPdfPath     = "output.pdf";         // result PDF
        const string portfolioPdfPath  = "portfolio.pdf";      // PDF portfolio to embed

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

        // Open the source document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Embed the PDF portfolio as rich media content.
            // MIME type for PDF is "application/pdf".
            using (FileStream portfolioStream = File.OpenRead(portfolioPdfPath))
            {
                richMedia.SetContent("application/pdf", portfolioStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Disable printing by setting permissions that do NOT include PrintDocument.
            // Here we allow content extraction but forbid printing.
            Permissions perms = Permissions.ExtractContent; // adjust as needed
            doc.Encrypt(userPassword: "", ownerPassword: "", perms, CryptoAlgorithm.AESx256);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMedia annotation added and document saved to '{outputPdfPath}'.");
    }
}