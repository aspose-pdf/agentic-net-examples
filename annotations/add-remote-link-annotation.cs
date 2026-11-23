using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Actions, annotations and related types live here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string remotePdf = "Appendix.pdf";
        const int remotePage = 3;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Remote PDF may be missing at runtime; the link will still be created.
        using (Document doc = new Document(inputPath))
        {
            // Use the first page; change index if needed.
            Page page = doc.Pages[1];

            // Define the clickable area (lower‑left x, lower‑left y, upper‑right x, upper‑right y).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);

            // Create the link annotation.
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Configure the remote GoTo action using the constructor that accepts file path and page number.
            GoToRemoteAction remoteAction = new GoToRemoteAction(remotePdf, remotePage);
            link.Action = remoteAction;

            // Optional visual styling.
            link.Border = new Border(link) { Width = 1 };
            link.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page.
            page.Annotations.Add(link);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with remote link to '{outputPath}'.");
    }
}
