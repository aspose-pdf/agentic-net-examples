using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF to which the annotation will be added
        const string portfolioPdf = "portfolio.pdf";  // PDF portfolio to embed
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(portfolioPdf))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the base PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation (constructor rule)
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // NOTE: In recent Aspose.PDF versions the ActivateOn property is optional
            // and the default activation (page open) is sufficient. The enum member "PO"
            // is no longer exposed, so we omit the assignment.

            // Embed the PDF portfolio as the content stream
            using (FileStream fs = File.OpenRead(portfolioPdf))
            {
                richMedia.SetContent(Path.GetFileName(portfolioPdf), fs);
            }

            // Optional visual styling
            richMedia.Color = Aspose.Pdf.Color.Blue;
            richMedia.Border = new Border(richMedia) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified document (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}
