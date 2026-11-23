using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "artifacts_page2.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (rule: page-indexing-one-based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than 2 pages.");
                return;
            }

            Page pageTwo = doc.Pages[2];

            // Open CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("LLX,LLY,URX,URY");

                // Iterate over artifacts on page two
                foreach (Artifact artifact in pageTwo.Artifacts)
                {
                    // Artifact.Rectangle is of type Aspose.Pdf.Rectangle
                    Aspose.Pdf.Rectangle rect = artifact.Rectangle;

                    // Write rectangle coordinates using invariant culture to ensure '.' as decimal separator
                    string line = string.Format(CultureInfo.InvariantCulture,
                                                "{0},{1},{2},{3}",
                                                rect.LLX,
                                                rect.LLY,
                                                rect.URX,
                                                rect.URY);
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"Artifact rectangles from page 2 have been written to '{outputCsvPath}'.");
        }
    }
}