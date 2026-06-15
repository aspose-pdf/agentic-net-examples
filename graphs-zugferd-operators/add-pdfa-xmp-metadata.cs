using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF document
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add XMP metadata required for PDF/A compliance
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Add Document ID (XMP) entry
            pdfDoc.Metadata.Add("xmp:DocumentID", "uuid:12345678-1234-1234-1234-123456789abc");
            // Add Creator Tool (XMP) entry
            pdfDoc.Metadata.Add("xmp:CreatorTool", "Aspose.Pdf for .NET");

            // Optionally set standard document information fields
            pdfDoc.Info.Creator = "Aspose.Pdf for .NET";
            pdfDoc.Info.Title = "Sample PDF/A compliant document";

            // Save the updated PDF
            pdfDoc.Save("output.pdf");
        }
    }
}