using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the link will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Open attached PDF"
            };

            // Set the action to open the external PDF file at its first page
            // GoToRemoteAction opens a PDF document (remotePdf) and jumps to a page number.
            link.Action = new GoToRemoteAction(attachment, 1);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified document (lifecycle rule: use using, then Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Link annotation added. Output saved to '{outputPdf}'.");
    }
}