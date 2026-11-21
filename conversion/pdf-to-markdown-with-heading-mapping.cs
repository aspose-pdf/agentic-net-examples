using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputMdPath = "output.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            pdfDoc.Pages.Accept(absorber);

            // Custom heading level mapping based on font size (adjust thresholds as needed)
            var headingMap = new (double MinFontSize, string MarkdownPrefix)[]
            {
                (18.0, "# "),   // Level 1
                (14.0, "## "),  // Level 2
                (12.0, "### ")  // Level 3
            };

            StringBuilder markdownBuilder = new StringBuilder();

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                string line = fragment.Text.TrimEnd('\r', '\n');

                // Preserve blank lines in the output
                if (string.IsNullOrWhiteSpace(line))
                {
                    markdownBuilder.AppendLine();
                    continue;
                }

                // Determine heading level using the fragment's font size (via TextState)
                string prefix = string.Empty;
                double fontSize = fragment.TextState.FontSize; // <-- Fixed property access
                foreach (var (minSize, mdPrefix) in headingMap)
                {
                    if (fontSize >= minSize)
                    {
                        prefix = mdPrefix;
                        break; // Use the first matching (largest) level
                    }
                }

                markdownBuilder.AppendLine($"{prefix}{line}");
            }

            // Write the generated Markdown to a file
            File.WriteAllText(outputMdPath, markdownBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Markdown file created at '{outputMdPath}'.");
        }
    }
}
