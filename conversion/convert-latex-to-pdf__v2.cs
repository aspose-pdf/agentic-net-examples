using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string latexPath = "input.tex";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(latexPath))
        {
            Console.Error.WriteLine($"LaTeX file not found: {latexPath}");
            return;
        }

        // Load the LaTeX file using LatexLoadOptions
        LatexLoadOptions loadOptions = new LatexLoadOptions();

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(latexPath, loadOptions))
        {
            // Save the document as PDF (default format)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"LaTeX file converted and saved as PDF: {pdfPath}");
    }
}