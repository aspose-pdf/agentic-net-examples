using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextState and FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // 1. Create a new artifact and add it to the page
            // -------------------------------------------------
            // Use Background type/subtype (any valid combination can be used)
            Artifact artifact = new Artifact(Artifact.ArtifactType.Background, Artifact.ArtifactSubtype.Background);
            artifact.Text = "Created artifact";

            // Position the artifact (Point is in page coordinate space)
            artifact.Position = new Point(100, 700);

            artifact.IsBackground = false;   // place in front of page contents
            artifact.Opacity = 0.8;          // semi‑transparent

            // Define visual appearance of the text
            TextState ts = new TextState
            {
                FontSize = 14,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Color.Blue
            };
            artifact.TextState = ts;

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(artifact);

            // -------------------------------------------------
            // 2. Extract (enumerate) existing artifacts on the page
            // -------------------------------------------------
            Console.WriteLine("Existing artifacts on the page:");
            foreach (Artifact art in page.Artifacts)
            {
                Console.WriteLine($"- Text: {art.Text}, Opacity: {art.Opacity}");
            }

            // -------------------------------------------------
            // 3. Modify the first artifact (if any)
            // -------------------------------------------------
            if (page.Artifacts.Count > 0)
            {
                // Artifact collections are 1‑based
                Artifact first = page.Artifacts[1];
                first.BeginUpdates();               // batch updates for performance
                first.Text = "Modified artifact text";
                first.Opacity = 0.5;
                first.SaveUpdates();                // commit changes
            }

            // Save the modified PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Artifacts processed and saved to '{outputPath}'.");
    }
}