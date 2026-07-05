using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF to which the link will be added
        const string outputPdf = "output.pdf";        // Resulting PDF
        const string externalPdf = "external.pdf";    // Target PDF to open
        const int externalPage = 3;                   // Page number in the target PDF

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(externalPdf))
        {
            Console.Error.WriteLine($"External PDF not found: {externalPdf}");
            return;
        }

        // Load the source document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue, // Visual cue for the link
                Contents = $"Open page {externalPage} of external.pdf"
            };

            // Assign a remote go‑to action that opens the external PDF at the specified page
            link.Action = new GoToRemoteAction(externalPdf, externalPage);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Link annotation added. Output saved to '{outputPdf}'.");
    }
}