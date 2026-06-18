using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output LaTeX file
        const string pdfPath = "input.pdf";
        const string texPath = "output.tex";

        // Custom LaTeX macro definitions for special symbols
        // Adjust these definitions as needed for your document
        string customMacros = @"
% Custom macro definitions
\newcommand{\specialAlpha}{\alpha}
\newcommand{\specialBeta}{\beta}
% Add more macros here
";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize TeXSaveOptions (no special properties required)
                TeXSaveOptions saveOptions = new TeXSaveOptions();

                // Save the PDF as LaTeX (TeX) using the save options
                pdfDocument.Save(texPath, saveOptions);
            }

            // Read the generated TeX file, prepend custom macro definitions, and overwrite the file
            string originalTex = File.ReadAllText(texPath);
            string finalTex = customMacros + Environment.NewLine + originalTex;
            File.WriteAllText(texPath, finalTex);

            Console.WriteLine($"PDF successfully converted to LaTeX with custom macros: '{texPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}