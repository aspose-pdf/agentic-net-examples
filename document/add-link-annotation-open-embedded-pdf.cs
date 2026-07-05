using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPdf = "output.pdf";

        // Verify that source PDF and attachment exist
        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // Load the source PDF (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Embed the PDF attachment into the document
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            doc.EmbeddedFiles.Add(fileSpec);

            // Define the clickable area for the link annotation
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a LinkAnnotation on the first page
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,          // Visual cue for the link
                Contents = "Open attached PDF"           // Tooltip text
            };

            // Set the action to open the attached PDF (remote go‑to action)
            link.Action = new GoToRemoteAction(attachmentPath, 1);

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdf}'.");
    }
}