using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string remotePdfPath = "remote.pdf";
        const int remotePageNumber = 2; // page to open in the external PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,          // visual color of the annotation border
                Contents = "Open remote PDF page"        // tooltip text
            };

            // Assign a remote go-to action that opens the specified page of the external PDF
            link.Action = new GoToRemoteAction(remotePdfPath, remotePageNumber);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}