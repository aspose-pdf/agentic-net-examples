using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf"; // path to source PDF
        const string outputDir = "latex-output"; // desired output directory

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Output .tex file path (file name can be chosen as needed)
        string texPath = Path.Combine(outputDir, "output.tex");

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure TeXSaveOptions (used for LaTeX export)
            TeXSaveOptions saveOptions = new TeXSaveOptions();
            saveOptions.OutDirectoryPath = outputDir; // specify output directory

            // Save as LaTeX (.tex)
            pdfDoc.Save(texPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to LaTeX: {texPath}");
    }
}