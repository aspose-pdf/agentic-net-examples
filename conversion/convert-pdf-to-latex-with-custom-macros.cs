using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTex = "output.tex";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF and extract its text using TextAbsorber (correct API)
        using (Document pdfDoc = new Document(inputPdf))
        {
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            pdfDoc.Pages.Accept(absorber);
            string rawText = absorber.Text;

            // Convert extracted text to LaTeX with custom macro definitions
            string latexContent = GenerateLatex(rawText);

            // Write the LaTeX source to a .tex file
            File.WriteAllText(outputTex, latexContent);
        }

        Console.WriteLine($"LaTeX file saved to '{outputTex}'.");
    }

    // Builds a LaTeX document string, defines macros, and escapes special characters
    static string GenerateLatex(string plainText)
    {
        // Custom macro definitions for special symbols
        string macros = @"\newcommand{\myAlpha}{\ensuremath{\alpha}}
\newcommand{\myBeta}{\ensuremath{\beta}}
% Add additional macro definitions here
";

        // Escape LaTeX‑sensitive characters
        string escaped = EscapeLatex(plainText);

        // Replace literal Unicode symbols with the defined macros
        escaped = escaped.Replace("α", @"\myAlpha");
        escaped = escaped.Replace("β", @"\myBeta");

        // Assemble the complete LaTeX document
        return @"\documentclass{article}
\usepackage{amsmath}
" + macros + @"
\begin{document}
" + escaped + @"
\end{document}";
    }

    // Escapes characters that have special meaning in LaTeX
    static string EscapeLatex(string text)
    {
        return text
            .Replace(@"\", @"\textbackslash{}")
            .Replace("{", @"\{")
            .Replace("}", @"\}")
            .Replace("#", @"\#")
            .Replace("$", @"\$")
            .Replace("%", @"\%")
            .Replace("^", @"\^{}")
            .Replace("_", @"\_")
            .Replace("~", @"\~{}")
            .Replace("&", @"\&");
    }
}