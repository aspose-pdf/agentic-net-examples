using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "artifacts_page9.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 9 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document has fewer than 9 pages.");
                return;
            }

            // Retrieve the 9th page
            Page pageNine = doc.Pages[9];

            // Open a CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                // Write CSV header
                writer.WriteLine("ArtifactIndex,Subtype");

                // Iterate over artifacts on the page
                // ArtifactCollection implements IEnumerable, so we can use a simple foreach
                int index = 1;
                foreach (Artifact artifact in pageNine.Artifacts)
                {
                    // Artifact.Subtype returns an enum; convert to string for CSV
                    string subtype = artifact.Subtype.ToString();
                    writer.WriteLine($"{index},{subtype}");
                    index++;
                }
            }

            Console.WriteLine($"Artifact mapping for page 9 written to '{outputCsvPath}'.");
        }
    }
}