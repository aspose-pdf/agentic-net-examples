using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages from which the Bates numbering stamp should be removed (1‑based indexes)
        var pagesToRemove = new HashSet<int> { 2, 4, 7 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Step 1 – Remove all Bates numbering artifacts from the whole document.
            // The core API provides DeleteBatesNumbering() as an extension method
            // on PageCollection. This removes every Bates numbering artifact from
            // each page.
            // -----------------------------------------------------------------
            doc.Pages.DeleteBatesNumbering();

            // -----------------------------------------------------------------
            // Step 2 – Re‑add Bates numbering to pages that should keep it.
            // Because the core API does not expose a method to delete a single
            // Bates artifact, we delete all and then add the artifact back only
            // to the pages that are NOT in the removal list.
            // -----------------------------------------------------------------
            // Create a configured Bates numbering artifact (customize as needed)
            BatesNArtifact batesArtifact = new BatesNArtifact
            {
                StartNumber      = 1,          // starting number
                NumberOfDigits   = 6,          // e.g., 000001
                Prefix           = "Bates-",   // optional prefix
                ArtifactHorizontalAlignment = HorizontalAlignment.Right,
                ArtifactVerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin = 20,            // distance from bottom edge
                RightMargin  = 20             // distance from right edge
            };

            // Add the artifact only to pages that are NOT marked for removal
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (!pagesToRemove.Contains(i))
                {
                    // The AddBatesNumbering overload that accepts a pre‑configured
                    // artifact adds the same artifact instance to every page in the
                    // collection, so we need to clone it for each page to avoid shared
                    // state. Creating a new instance per page is the safest approach.
                    BatesNArtifact artifactForPage = new BatesNArtifact
                    {
                        StartNumber      = batesArtifact.StartNumber,
                        NumberOfDigits   = batesArtifact.NumberOfDigits,
                        Prefix           = batesArtifact.Prefix,
                        ArtifactHorizontalAlignment = batesArtifact.ArtifactHorizontalAlignment,
                        ArtifactVerticalAlignment   = batesArtifact.ArtifactVerticalAlignment,
                        BottomMargin = batesArtifact.BottomMargin,
                        RightMargin  = batesArtifact.RightMargin
                    };

                    // Add the artifact to the specific page
                    doc.Pages[i].Artifacts.Add(artifactForPage);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering removed from pages {string.Join(", ", pagesToRemove)} and saved to '{outputPath}'.");
    }
}