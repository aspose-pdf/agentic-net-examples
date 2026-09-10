using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputTexPath = "output.tex";

        // Custom LaTeX macro definitions (example)
        // These lines will be inserted at the beginning of the generated .tex file.
        string customMacros = @"
% Custom macro definitions for special symbols
\newcommand{\AlphaSym}{\ensuremath{\alpha}}
\newcommand{\BetaSym}{\ensuremath{\beta}}
";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare TeX save options (default constructor is sufficient)
                TeXSaveOptions texSaveOptions = new TeXSaveOptions();

                // Save the PDF as a TeX file
                pdfDoc.Save(outputTexPath, texSaveOptions);
            }

            // Insert custom macro definitions at the top of the generated .tex file
            // Read the generated content
            string texContent = File.ReadAllText(outputTexPath);

            // Prepend the macro definitions
            string finalTex = customMacros + Environment.NewLine + texContent;

            // Write back to the same file (or to a new file if preferred)
            File.WriteAllText(outputTexPath, finalTex);

            Console.WriteLine($"PDF successfully converted to LaTeX: {outputTexPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
