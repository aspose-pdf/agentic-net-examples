using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToMarkdownConverter
{
    // Simple heuristic thresholds for heading detection (in points).
    // Adjust these values according to the typical font sizes used in your PDFs.
    private const double HeadingLevel1Threshold = 16.0; // >= 16pt => # Heading
    private const double HeadingLevel2Threshold = 14.0; // >= 14pt => ## Heading
    private const double HeadingLevel3Threshold = 12.0; // >= 12pt => ### Heading

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputMdPath = "output.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            var sb = new StringBuilder();

            // Process each page sequentially.
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Use TextFragmentAbsorber to get individual text fragments.
                var absorber = new TextFragmentAbsorber();
                // Pure extraction keeps the original formatting (font size, etc.).
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);

                // The TextFragmentAbsorber returns fragments in reading order.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Determine markdown heading level based on font size.
                    string markdownLine = ConvertFragmentToMarkdown(fragment);
                    sb.AppendLine(markdownLine);
                }
            }

            // Write the generated markdown to the output file.
            File.WriteAllText(outputMdPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: {outputMdPath}");
    }

    /// <summary>
    /// Converts a single TextFragment to a markdown line using simple font‑size heuristics.
    /// </summary>
    private static string ConvertFragmentToMarkdown(TextFragment fragment)
    {
        // Trim the fragment text to avoid stray line‑breaks.
        string text = fragment.Text.Trim();
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        double fontSize = fragment.TextState.FontSize;
        string prefix = string.Empty;

        if (fontSize >= HeadingLevel1Threshold)
            prefix = "# ";
        else if (fontSize >= HeadingLevel2Threshold)
            prefix = "## ";
        else if (fontSize >= HeadingLevel3Threshold)
            prefix = "### ";
        // For smaller fonts we treat the text as a normal paragraph.
        // Preserve line breaks that were originally present in the fragment.
        return prefix + text;
    }
}
