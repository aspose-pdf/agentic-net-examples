using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";          // PDF to which the annotation will be added
        const string portfolioPdfPath = "portfolio.pdf";   // PDF portfolio to embed
        const string outputPdfPath = "output_with_richmedia.pdf";

        if (!File.Exists(inputPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load the document to be annotated
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will appear (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set the content type – using Video as a generic container (RichMedia supports only Audio/Video)
            richMedia.Type = RichMediaAnnotation.ContentType.Video;

            // Embed the PDF portfolio as the rich media content.
            // The first argument is the MIME type, the second is the stream containing the file.
            using (FileStream portfolioStream = File.OpenRead(portfolioPdfPath))
            {
                richMedia.SetContent("application/pdf", portfolioStream);
            }

            // Optionally set a title/description for the annotation
            richMedia.Contents = "Embedded PDF Portfolio";

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Disable printing by encrypting the document with permissions that *exclude* PrintDocument.
            // Any permission not granted is denied.
            Permissions permissions = Permissions.ModifyContent |
                                      Permissions.ExtractContent |
                                      Permissions.ModifyTextAnnotations |
                                      Permissions.FillForm |
                                      Permissions.AssembleDocument |
                                      Permissions.PrintingQuality; // PrintingQuality alone does not allow full printing

            // Empty strings for user/owner passwords keep the document open without prompting,
            // but still enforce the permission set.
            doc.Encrypt("", "", permissions, CryptoAlgorithm.RC4x128);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMedia annotation added and saved to '{outputPdfPath}'.");
    }
}
