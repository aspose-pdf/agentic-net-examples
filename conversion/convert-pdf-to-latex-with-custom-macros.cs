using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Save as LaTeX using TeXSaveOptions
            TeXSaveOptions texOptions = new TeXSaveOptions();
            pdfDoc.Save(outputTex, texOptions);
        }

        // Custom LaTeX macro definitions for special symbols
        string customMacros = @"% Custom macro definitions
\newcommand{\alphaSym}{\alpha}
\newcommand{\betaSym}{\beta}
% Add additional macros here as needed

";

        // Prepend the custom macros to the generated .tex file
        string originalContent = File.ReadAllText(outputTex);
        File.WriteAllText(outputTex, customMacros + originalContent);

        Console.WriteLine($"PDF successfully converted to LaTeX with custom macros: {outputTex}");
    }
}