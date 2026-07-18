using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        string inputPdfPath = "input.pdf";

        // Directory where the LaTeX files will be written.
        string outputDirectory = "output";

        // Full path for the main LaTeX output file.
        string outputTexPath = Path.Combine(outputDirectory, "output.tex");

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize TeXSaveOptions and set the output directory.
            Aspose.Pdf.TeXSaveOptions texSaveOptions = new Aspose.Pdf.TeXSaveOptions
            {
                OutDirectoryPath = outputDirectory
            };

            // Save the PDF as a LaTeX (.tex) file.
            pdfDocument.Save(outputTexPath, texSaveOptions);
        }

        Console.WriteLine($"PDF successfully converted to LaTeX at '{outputTexPath}'.");
    }
}