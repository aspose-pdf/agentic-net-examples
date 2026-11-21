using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "LatexOutput";
        // Path.Combine cannot be used in a const declaration, so use a regular variable.
        string outputTexPath = Path.Combine(outputDir, "output.tex");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // TeXSaveOptions does not expose an OutputDirectory property.
                // The output directory is already part of the output file path passed to Save.
                TeXSaveOptions texOptions = new TeXSaveOptions();

                // Save the PDF as LaTeX (TeX) using the specified options
                pdfDoc.Save(outputTexPath, texOptions);
            }

            Console.WriteLine($"PDF successfully converted to LaTeX at: {outputTexPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
