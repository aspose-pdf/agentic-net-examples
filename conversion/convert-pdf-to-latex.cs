using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Path to the source PDF file.
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Directory where LaTeX files will be written.
        string outDir = Path.Combine(dataDir, "LatexOutput");

        // Full path for the generated .tex file.
        string texPath = Path.Combine(outDir, "output.tex");

        // Verify the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outDir);

        // Load the PDF and convert it to LaTeX using TeXSaveOptions.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize save options for TeX (LaTeX) export.
            TeXSaveOptions saveOptions = new TeXSaveOptions
            {
                // Specify the directory where auxiliary files will be placed.
                OutDirectoryPath = outDir
            };

            // Save the document as a .tex file using the specified options.
            pdfDoc.Save(texPath, saveOptions);
        }

        Console.WriteLine($"LaTeX file successfully saved to: {texPath}");
    }
}