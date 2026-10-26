using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const string externalPdfPath = "external.pdf";
        const int externalPageNumber = 3; // page to open in the external PDF (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(externalPdfPath))
        {
            Console.Error.WriteLine($"External PDF not found: {externalPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Choose the page where the link annotation will be placed (first page in this example)
                Page page = doc.Pages[1];

                // Define the clickable rectangle (coordinates are in points; lower‑left to upper‑right)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

                // Create the link annotation on the selected page
                LinkAnnotation link = new LinkAnnotation(page, rect);

                // Configure the remote go‑to action to open a specific page in another PDF
                // GoToRemoteAction does not have a parameterless constructor nor settable properties in this API version.
                // Use the constructor that accepts the target file path and the destination page number.
                GoToRemoteAction remoteAction = new GoToRemoteAction(externalPdfPath, externalPageNumber);
                link.Action = remoteAction;

                // Optional visual styling for the annotation
                link.Color = Aspose.Pdf.Color.Blue;
                link.Border = new Border(link) { Width = 1 };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(link);

                // Save the modified document (PDF format by default)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with link annotation: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
