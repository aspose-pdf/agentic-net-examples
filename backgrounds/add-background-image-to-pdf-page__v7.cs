using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string backgroundPath = "background.jpg";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Verify the document has at least six pages (1‑based indexing)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document contains fewer than 6 pages.");
                return;
            }

            // Load the background image into a memory stream (kept open until Save)
            byte[] imgBytes = File.ReadAllBytes(backgroundPath);
            using (MemoryStream imgStream = new MemoryStream(imgBytes))
            {
                // Create a BackgroundArtifact and assign the image stream
                BackgroundArtifact bgArtifact = new BackgroundArtifact
                {
                    BackgroundImage = imgStream
                };

                // Attach the artifact to page 6
                Page pageSix = doc.Pages[6];
                pageSix.Artifacts.Add(bgArtifact);

                // Save the modified PDF while the stream is still open
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with background applied to page 6: {outputPdfPath}");
    }
}
