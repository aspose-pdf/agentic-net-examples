using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for source PDF, output PDF, XFDF file and re‑imported PDF
        const string sourcePdfPath = "source.pdf";
        const string pdfWithLinkPath = "with_link.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string importedPdfPath = "imported.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the source PDF and add a link annotation (external URL)
        // ------------------------------------------------------------
        using (Document doc = new Document(sourcePdfPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the link will appear
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Set a visible border color (optional)
                Color = Aspose.Pdf.Color.Blue,
                // Tooltip text shown when hovering
                Contents = "Visit Aspose.Pdf website"
            };

            // Assign an action that opens an external URL
            link.Action = new GoToURIAction("https://www.aspose.com/pdf");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF that now contains the link annotation
            doc.Save(pdfWithLinkPath);
        }

        // ------------------------------------------------------------
        // 2. Export all annotations (including the link) to XFDF (XML)
        // ------------------------------------------------------------
        using (Document doc = new Document(pdfWithLinkPath))
        {
            // Export annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ------------------------------------------------------------
        // 3. Load a fresh copy of the original PDF and import the XFDF
        // ------------------------------------------------------------
        using (Document doc = new Document(sourcePdfPath))
        {
            // Import the previously exported annotations
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the PDF that now has the imported link annotation
            doc.Save(importedPdfPath);
        }

        Console.WriteLine("Link annotation added, exported to XFDF, and re‑imported successfully.");
    }
}