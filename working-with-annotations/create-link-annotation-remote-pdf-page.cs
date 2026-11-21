using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";          // PDF to which the link will be added
        const string targetPath  = "target.pdf";         // External PDF to open
        const string outputPath  = "output_with_link.pdf";
        const int    targetPage  = 3;                    // Page number in the external PDF (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (rule: page-indexing-one-based)
            Page page = doc.Pages[1];

            // Define the clickable rectangle area (coordinates in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a link annotation on the specified rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Optional visual styling
            link.Border = new Border(link) { Width = 1 };
            link.Color  = Aspose.Pdf.Color.Blue;

            // Create a remote-go-to action that opens the external PDF at the desired page
            GoToRemoteAction remoteAction = new GoToRemoteAction(targetPath, targetPage);
            link.Action = remoteAction;

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified document (no PreSave needed for annotations)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Output saved to '{outputPath}'.");
    }
}