using System;
using System.IO;
using Aspose.Pdf; // All save options, including TeXSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Desired output directory for LaTeX resources
        const string outputDirectory = "output";

        // Full path of the resulting .tex file
        string texFilePath = Path.Combine(outputDirectory, "output.tex");

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create TeXSaveOptions (the LaTeX save options)
            TeXSaveOptions texOptions = new TeXSaveOptions();

            // Specify the output directory where auxiliary files will be written
            texOptions.OutDirectoryPath = outputDirectory;

            // Save the PDF as a LaTeX (.tex) file using the options
            pdfDocument.Save(texFilePath, texOptions);
        }

        Console.WriteLine($"LaTeX file saved to '{texFilePath}'.");
    }
}