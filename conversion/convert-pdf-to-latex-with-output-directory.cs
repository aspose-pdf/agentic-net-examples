using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes TeXSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Desired output directory for LaTeX files
        const string outputDir = "LatexOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Full path for the generated .tex file (name can be chosen as needed)
        string texFilePath = Path.Combine(outputDir, "output.tex");

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, convert to LaTeX (TeX) and save
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize TeXSaveOptions and set the output directory
            TeXSaveOptions saveOptions = new TeXSaveOptions
            {
                OutDirectoryPath = outputDir
            };

            // Save as LaTeX (.tex) using the specified options
            pdfDocument.Save(texFilePath, saveOptions);
        }

        Console.WriteLine($"LaTeX file saved to: {texFilePath}");
    }
}