using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Load the source PDF (the page to be absorbed)
        using (Document srcDoc = new Document(sourcePath))
        // Load the target PDF (where the page will be integrated)
        using (Document tgtDoc = new Document(targetPath))
        {
            // Select the page to absorb (using 1‑based indexing)
            Page pageToAbsorb = srcDoc.Pages[1];

            // Add a new blank page to the target document
            Page newPage = tgtDoc.Pages.Add();

            // Create an artifact and set the PDF page as its content
            Artifact artifact = new Artifact(Artifact.ArtifactType.Background, Artifact.ArtifactSubtype.Background);
            artifact.SetPdfPage(pageToAbsorb);

            // Attach the artifact to the new page
            newPage.Artifacts.Add(artifact);

            // Save the combined document
            tgtDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}