using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Define the files to attach together with their MIME types and descriptions
        var attachments = new (string Path, string Mime, string Description)[]
        {
            ("file1.txt", "text/plain", "First text file"),
            ("image1.png", "image/png", "Sample image"),
            ("data.csv",  "text/csv",  "CSV data file")
        };

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each attachment file exists
        foreach (var att in attachments)
        {
            if (!File.Exists(att.Path))
            {
                Console.Error.WriteLine($"Attachment not found: {att.Path}");
                return;
            }
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose a page to place the annotations (first page in this example)
            Page page = doc.Pages[1];

            // Base rectangle coordinates for the first annotation
            double llx = 100, lly = 700, urx = 150, ury = 750;
            double verticalOffset = 0;

            foreach (var att in attachments)
            {
                // Create a FileSpecification with description (MIME type is not settable via API)
                FileSpecification fileSpec = new FileSpecification(att.Path, att.Description);

                // Position rectangle for this annotation (stacked vertically)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    llx,
                    lly - verticalOffset,
                    urx,
                    ury - verticalOffset);

                // Create the file attachment annotation
                FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual settings
                    Icon = FileIcon.PushPin,
                    Title = Path.GetFileName(att.Path)
                };

                // Add the annotation to the page
                page.Annotations.Add(fileAnn);

                // Increment offset for the next annotation
                verticalOffset += 60;
            }

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdf}");
    }
}
