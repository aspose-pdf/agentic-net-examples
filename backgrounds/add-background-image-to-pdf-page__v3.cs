using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for BackgroundArtifact

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "background.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open the PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify the document has at least two pages (1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document contains fewer than two pages.");
                return;
            }

            // Load the image stream and create a BackgroundArtifact
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // BackgroundImage expects a System.IO.Stream, so assign the stream directly.
                BackgroundArtifact bgArtifact = new BackgroundArtifact
                {
                    BackgroundImage = imgStream
                };

                // Attach the artifact to page 2 (1‑based index)
                Page pageTwo = doc.Pages[2];
                pageTwo.Artifacts.Add(bgArtifact);

                // Save while the image stream is still open
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
