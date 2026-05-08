using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for FileHyperlink (inherits Hyperlink)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_with_link.pdf";
        const string attachment = "attachment.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Embed the attachment into the PDF using FileSpecification (the correct overload)
            var fileSpec = new FileSpecification(attachment, "Embedded Attachment");
            // Ensure the file contents are read from the stream (optional, but guarantees embedding)
            using (FileStream attStream = File.OpenRead(attachment))
            {
                // Assign the stream to the FileSpecification.Contents property
                fileSpec.Contents = attStream;
            }
            // Add the FileSpecification to the document's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Choose the page where the link will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (coordinates: llx, lly, urx, ury)
            // Aspose.Pdf.Rectangle expects float values; using literals works but we cast for clarity
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                (float)100, (float)500, (float)300, (float)550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Open attached PDF"
            };

            // Set the hyperlink to the embedded file using FileHyperlink (file name only)
            link.Hyperlink = new FileHyperlink(Path.GetFileName(attachment));

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdf}'.");
    }
}
