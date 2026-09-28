using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string latexPath = "input.tex";   // Path to the LaTeX source file
        const string pdfPath   = "output.pdf";  // Desired PDF output path

        // Verify the LaTeX file exists before proceeding
        if (!File.Exists(latexPath))
        {
            Console.Error.WriteLine($"LaTeX file not found: {latexPath}");
            return;
        }

        try
        {
            // Load the LaTeX document using the constructor that accepts TeXLoadOptions.
            var loadOptions = new TeXLoadOptions();
            using (var doc = new Document(latexPath, loadOptions))
            {
                // Save directly to PDF. No additional SaveOptions are required.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"LaTeX successfully converted to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Report any errors (parsing failures, missing fonts, etc.)
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
