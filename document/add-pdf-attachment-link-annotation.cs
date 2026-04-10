using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for Hyperlink types

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output.pdf";         // result PDF
        const string attachment = "attachment.pdf";     // PDF to open on click

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the link will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);

            // Create a LinkAnnotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Set a visible border color (optional)
                Color = Aspose.Pdf.Color.Blue,
                // Set the hyperlink to a FileHyperlink that points to the attachment PDF
                Hyperlink = new FileHyperlink(attachment)
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Link annotation added. Output saved to '{outputPdf}'.");
    }
}