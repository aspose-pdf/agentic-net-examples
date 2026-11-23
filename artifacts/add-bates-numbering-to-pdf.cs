using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple DTO to hold pagination settings – we cannot instantiate the abstract
    // Aspose.Pdf.PaginationArtifact, so we define our own lightweight version.
    private class PaginationArtifactInfo
    {
        public string Prefix { get; set; } = string.Empty;
        public int StartNumber { get; set; } = 1;
        public int NumberOfDigits { get; set; } = 1;
        public string Suffix { get; set; } = string.Empty;
        // FontSize is defined as float because Aspose.Pdf.Text.TextState.FontSize expects a float.
        public float FontSize { get; set; } = 12f;
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the batch of pagination artifacts.
        PaginationArtifactInfo[] artifacts = new PaginationArtifactInfo[]
        {
            new PaginationArtifactInfo
            {
                Prefix = "DOC-",
                StartNumber = 1,
                NumberOfDigits = 4,
                Suffix = "",
                FontSize = 10f
            },
            new PaginationArtifactInfo
            {
                Prefix = "SEC-",
                StartNumber = 100,
                NumberOfDigits = 3,
                Suffix = "-END",
                FontSize = 10f
            }
        };

        // Load the source PDF using the core Document API.
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Apply each artifact sequentially. The text is added as a TextFragment on every page.
        foreach (var art in artifacts)
        {
            int currentNumber = art.StartNumber;
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                string numberString = currentNumber.ToString().PadLeft(art.NumberOfDigits, '0');
                string fullText = $"{art.Prefix}{numberString}{art.Suffix}";

                TextFragment tf = new TextFragment(fullText)
                {
                    // Position the fragment near the bottom‑right corner. Adjust as needed.
                    Position = new Position(500, 20), // X,Y in points (approx. page width‑margin, margin from bottom)
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                };
                // Set font size via TextState. Cast to float because FontSize expects a float.
                tf.TextState.FontSize = art.FontSize;

                doc.Pages[pageNum].Paragraphs.Add(tf);
                currentNumber++;
            }
        }

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
    }
}
