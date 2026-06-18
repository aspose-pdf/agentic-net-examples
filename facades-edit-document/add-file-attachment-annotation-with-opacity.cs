using System;
using System.Drawing;                     // needed for rectangle calculations
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // result PDF
        const string attachFile = "sample.txt";    // file to attach

        // Ensure the source PDF exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Verify that page 3 exists
        if (doc.Pages.Count < 3)
        {
            Console.Error.WriteLine("The document has less than 3 pages.");
            return;
        }

        // Define the annotation rectangle.
        // Aspose.Pdf.Rectangle expects (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
        double x = 100;      // left
        double y = 500;      // bottom
        double width = 200;  // width
        double height = 100; // height
        Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

        // Prepare the file specification for the attachment.
        FileSpecification fileSpec = new FileSpecification(attachFile, "Sample attachment file");

        // Get the target page (pages are 1‑based).
        Page targetPage = doc.Pages[3];

        // Create a file‑attachment annotation with 0.6 opacity.
        FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(targetPage, annotRect, fileSpec)
        {
            Contents = "Attached sample file",
            Icon = FileIcon.Graph,          // valid icons: Graph, PushPin, Paperclip, Tag
            Opacity = 0.6f                  // 0 = fully transparent, 1 = fully opaque
        };

        // Add the annotation to page 3.
        targetPage.Annotations.Add(fileAnnot);

        // Save the modified PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Annotation added with 0.6 opacity on page 3. Saved to '{outputPdf}'.");
    }
}