using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTexPath = "output.tex";

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
                // Initialize TeX save options
                TeXSaveOptions texSaveOptions = new TeXSaveOptions();

                // Save the PDF as LaTeX (TeX) file
                pdfDoc.Save(outputTexPath, texSaveOptions);
            }

            // Define custom LaTeX macros for special symbols
            string customMacros = @"
% Custom macro definitions for special symbols
\newcommand{\specialsymbolA}{\ensuremath{\alpha}}
\newcommand{\specialsymbolB}{\ensuremath{\beta}}
";

            // Read the generated TeX content
            string texContent = File.ReadAllText(outputTexPath);

            // Prepend the custom macros to the TeX file
            string finalTexContent = customMacros + Environment.NewLine + texContent;

            // Write the updated content back to the file
            File.WriteAllText(outputTexPath, finalTexContent);

            Console.WriteLine($"PDF successfully converted to LaTeX with custom macros: '{outputTexPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}