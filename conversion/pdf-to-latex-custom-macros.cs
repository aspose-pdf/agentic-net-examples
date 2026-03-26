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

        // Convert PDF to TeX (LaTeX) using Aspose.Pdf
        using (Document pdfDoc = new Document(inputPdf))
        {
            TeXSaveOptions texOptions = new TeXSaveOptions();
            pdfDoc.Save(outputTex, texOptions);
        }

        // Define custom LaTeX macros for special symbols
        string[] customMacros = new string[]
        {
            "\\newcommand{\\R}{\\mathbb{R}}",
            "\\newcommand{\\Z}{\\mathbb{Z}}",
            "\\newcommand{\\customsymbol}{\\textbf{★}}"
        };

        try
        {
            // Read the generated TeX file, prepend macros, and write back
            string texContent = File.ReadAllText(outputTex);
            string macrosBlock = string.Join(Environment.NewLine, customMacros) + Environment.NewLine + Environment.NewLine;
            File.WriteAllText(outputTex, macrosBlock + texContent);
            Console.WriteLine($"LaTeX file with custom macros saved to '{outputTex}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating TeX file: {ex.Message}");
        }
    }
}