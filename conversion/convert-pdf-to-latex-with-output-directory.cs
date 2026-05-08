using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Desired output directory for LaTeX files and resources
        const string outputDirectory = "output";

        // Full path of the resulting .tex file
        string outputTexPath = Path.Combine(outputDirectory, "output.tex");

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize TeXSaveOptions (non‑PDF format requires explicit options)
            TeXSaveOptions texOptions = new TeXSaveOptions
            {
                // Set the directory where auxiliary files (e.g., images) will be written
                OutDirectoryPath = outputDirectory
            };

            // Save the document as LaTeX (.tex) using the specified options
            pdfDocument.Save(outputTexPath, texOptions);
        }

        Console.WriteLine($"LaTeX file successfully saved to '{outputTexPath}'.");
    }
}