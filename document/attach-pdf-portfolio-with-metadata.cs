using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Annotations;        // For file attachment annotation (optional)

// Attach a PDF portfolio (embedded PDF) to an existing PDF and set custom description metadata.
class PortfolioAttacher
{
    static void Main()
    {
        const string sourcePdfPath      = "source.pdf";      // PDF to which the portfolio will be attached
        const string portfolioPdfPath   = "portfolio.pdf";   // PDF file to embed as a portfolio item
        const string outputPdfPath      = "output_with_portfolio.pdf";

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPdfPath}");
            return;
        }

        // Load the main document inside a using block (ensures proper disposal)
        using (Document doc = new Document(sourcePdfPath))
        {
            // -------------------------------------------------
            // 1. Create a FileSpecification for the portfolio PDF
            // -------------------------------------------------
            // The constructor accepts the file path; the file will be embedded.
            FileSpecification portfolioSpec = new FileSpecification(portfolioPdfPath);

            // Set a custom description (appears in the attachment panel of PDF viewers)
            portfolioSpec.Description = "Quarterly report portfolio – confidential";

            // Set the display name that will appear in the attachment list
            portfolioSpec.Name = Path.GetFileName(portfolioPdfPath);

            // -------------------------------------------------
            // 2. Add the FileSpecification to the document's embedded files collection
            // -------------------------------------------------
            doc.EmbeddedFiles.Add(portfolioSpec);

            // -------------------------------------------------
            // 3. (Optional) Add a visible file attachment annotation on the first page
            // -------------------------------------------------
            // This step creates an icon that users can click to open the embedded PDF.
            // If you only need the hidden attachment, you can skip this block.
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle iconRect = new Aspose.Pdf.Rectangle(100, 700, 150, 750);
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(firstPage, iconRect, portfolioSpec)
            {
                // Set annotation properties
                Title   = "Portfolio Attachment",
                Subject = "Embedded portfolio PDF",
                // Icon expects a FileIcon enum value
                Icon    = FileIcon.PushPin,
                Color   = Aspose.Pdf.Color.LightGray
            };
            firstPage.Annotations.Add(attachment);

            // -------------------------------------------------
            // 4. Set custom metadata (XMP) on the document
            // -------------------------------------------------
            // Add a custom key/value pair to the XMP metadata.
            doc.Metadata["CustomDescription"] = "This PDF contains an attached portfolio with quarterly data.";

            // You can also set standard document info fields if desired:
            doc.Info.Title  = "Report with Portfolio Attachment";
            doc.Info.Author = "Acme Corp";

            // -------------------------------------------------
            // 5. Save the updated PDF
            // -------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio attached and metadata set. Output saved to '{outputPdfPath}'.");
    }
}
