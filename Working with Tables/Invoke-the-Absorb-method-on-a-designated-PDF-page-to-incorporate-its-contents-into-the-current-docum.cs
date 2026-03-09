using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (page to be absorbed) and the target PDF (receiving document)
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "merged.pdf";

        // Verify that both input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Select the page from the source document that will be absorbed
            // Aspose.Pdf uses 1‑based indexing for pages
            Page pageToAbsorb = sourceDoc.Pages[1];

            // Create an artifact (background type) and set the selected page as its content
            Artifact artifact = new Artifact(Artifact.ArtifactType.Background, Artifact.ArtifactSubtype.Background);
            artifact.SetPdfPage(pageToAbsorb);

            // Add the artifact to a page in the target document (here we use the first page)
            targetDoc.Pages[1].Artifacts.Add(artifact);

            // Save the modified target document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}