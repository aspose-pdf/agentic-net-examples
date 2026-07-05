using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "image.jpg";
        const string outputPdfPath = "output.pdf";

        // Validate inputs
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Add the image as a document attachment (no visual annotation)
        // -----------------------------------------------------------------
        // Use a temporary PDF to hold the attachment before stamping.
        string tempPdfPath = Path.Combine(Path.GetTempPath(),
                                          Guid.NewGuid().ToString() + ".pdf");

        using (PdfContentEditor attachmentEditor = new PdfContentEditor())
        {
            // Load the original PDF
            attachmentEditor.BindPdf(inputPdfPath);

            // Attach the image file; description is optional
            attachmentEditor.AddDocumentAttachment(imagePath, "Embedded image attachment");

            // Save the intermediate PDF (now contains the attachment)
            attachmentEditor.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Add a stamp annotation that references the same image
        // -----------------------------------------------------------------
        using (PdfFileStamp stampFacade = new PdfFileStamp())
        {
            // Load the PDF that already has the attachment
            stampFacade.BindPdf(tempPdfPath);

            // Create a stamp object
            Aspose.Pdf.Facades.Stamp imageStamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the image to the stamp (the same file we attached)
            imageStamp.BindImage(imagePath);

            // Position and size of the stamp on the page
            imageStamp.SetOrigin(100, 500);          // X, Y coordinates (bottom‑left corner)
            imageStamp.SetImageSize(200, 200);       // Width, Height

            // Optional visual properties
            imageStamp.Opacity = 0.8f;               // Semi‑transparent
            imageStamp.IsBackground = false;        // Appear above page content

            // Add the stamp to the PDF (first page by default)
            stampFacade.AddStamp(imageStamp);

            // Save the final PDF with both the attachment and the stamp
            stampFacade.Save(outputPdfPath);
            stampFacade.Close();
        }

        // Clean up the temporary file
        try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF created: {outputPdfPath}");
    }
}