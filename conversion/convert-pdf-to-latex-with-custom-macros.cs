using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToLatexConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTexPath = "output.tex";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Extract all textual content from the PDF
            TextAbsorber absorber = new TextAbsorber();
            // Optional: preserve formatting (e.g., line breaks)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            pdfDoc.Pages.Accept(absorber);
            string rawText = absorber.Text ?? string.Empty;

            // Define custom LaTeX macros for special symbols
            // Key = symbol as it appears in the PDF text, Value = LaTeX macro
            var symbolMacros = new Dictionary<string, string>
            {
                { "α", @"\alpha" },
                { "β", @"\beta" },
                { "γ", @"\gamma" },
                { "Δ", @"\Delta" },
                { "π", @"\pi" },
                { "Ω", @"\Omega" },
                // Add more mappings as needed
            };

            // Replace each special symbol with its corresponding macro
            string latexBody = rawText;
            foreach (var kvp in symbolMacros)
            {
                latexBody = latexBody.Replace(kvp.Key, kvp.Value);
            }

            // Build the final LaTeX document content
            var latexLines = new List<string>
            {
                @"\documentclass{article}",
                @"\usepackage[utf8]{inputenc}",
                @"\usepackage{amsmath,amssymb}",
                // Define macros (if you want shorter names you can add them here)
                // Example: \newcommand{\myalpha}{\alpha}
                // In this case we directly use the standard macros, so no extra definitions needed
                @"\begin{document}",
                latexBody,
                @"\end{document}"
            };

            // Write the LaTeX content to the .tex file
            try
            {
                File.WriteAllLines(outputTexPath, latexLines);
                Console.WriteLine($"LaTeX file generated successfully: {outputTexPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write LaTeX file: {ex.Message}");
            }
        }
    }
}