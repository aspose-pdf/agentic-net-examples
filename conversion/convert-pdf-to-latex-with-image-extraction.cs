using System;
using System.IO;
using Aspose.Pdf;               // Document and all SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.tex";
        const string imagesDir  = "tempImages";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Ensure the directory for extracted images exists
        Directory.CreateDirectory(imagesDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // LaTeXSaveOptions is the correct SaveOptions subclass for LaTeX output
            LaTeXSaveOptions latexOpts = new LaTeXSaveOptions
            {
                // Set the folder where images extracted from the PDF will be written
                OutDirectoryPath = imagesDir
            };

            // Save the document as LaTeX; passing the options ensures non‑PDF format is respected
            doc.Save(outputPath, latexOpts);
        }

        Console.WriteLine($"PDF successfully converted to LaTeX: {outputPath}");
        Console.WriteLine($"Extracted images are stored in: {imagesDir}");
    }
}