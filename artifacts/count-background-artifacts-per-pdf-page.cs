using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string summaryPath = "summary.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create or overwrite the summary text file
            using (StreamWriter writer = new StreamWriter(summaryPath, false))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    var page = doc.Pages[i];
                    int artifactCount = 0;
                    foreach (var artifact in page.Artifacts)
                    {
                        if (artifact is BackgroundArtifact)
                            artifactCount++;
                    }
                    writer.WriteLine($"Page {i}: {artifactCount} background artifact(s)");
                }
            }
        }

        Console.WriteLine($"Summary written to '{summaryPath}'.");
    }
}
