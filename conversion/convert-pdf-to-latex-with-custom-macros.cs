using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTexPath = "output.tex";

        // Define custom LaTeX macro definitions for special symbols
        string customMacros = @"\newcommand{\specialsymbol}{\textsection}" + Environment.NewLine;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Save the document as LaTeX using TeXSaveOptions
            TeXSaveOptions texSaveOptions = new TeXSaveOptions();
            pdfDoc.Save(outputTexPath, texSaveOptions);
        }

        // Prepend the custom macro definitions to the generated .tex file
        string texContent = File.ReadAllText(outputTexPath);
        File.WriteAllText(outputTexPath, customMacros + texContent);

        Console.WriteLine($"Conversion completed. LaTeX file saved to '{outputTexPath}'.");
    }
}