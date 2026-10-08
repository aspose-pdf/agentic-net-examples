using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string signaturePath = "signature.png";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(signaturePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signaturePath}");
            return;
        }

        // Preserve the original PDF by working on a copy
        File.Copy(inputPdfPath, outputPdfPath, true);

        // Load the copied PDF using the high‑level Document API (PdfFileMend does not expose GetPageCount/InsertImage in recent versions)
        Document doc = new Document(outputPdfPath);

        // Prepare an ImageStamp that will be placed at the bottom‑left corner of each page
        ImageStamp signatureStamp = new ImageStamp(signaturePath)
        {
            // Position the stamp at the lower‑left corner (coordinates are relative to the page margins)
            // Setting LeftMargin and BottomMargin to 0 places it exactly at (0,0).
            LeftMargin = 0,
            BottomMargin = 0,
            // Keep the original image size; you can scale if required
            // Width and Height are optional – if omitted the stamp uses the image's native dimensions.
        };

        // Insert the stamp on every page
        foreach (Page page in doc.Pages)
        {
            // Add a clone of the stamp to avoid sharing the same instance across pages
            page.AddStamp(signatureStamp);
        }

        // Save the modified PDF
        doc.Save(outputPdfPath);

        Console.WriteLine($"Signature image inserted on all {doc.Pages.Count} pages. Output saved to '{outputPdfPath}'.");
    }
}
