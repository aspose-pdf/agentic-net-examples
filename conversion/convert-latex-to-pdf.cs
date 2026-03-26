using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the .tex file.
        string dataDir = "YOUR_DATA_DIRECTORY";
        string texFile = Path.Combine(dataDir, "sample.tex");
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Load the LaTeX file with TeXLoadOptions.
        TeXLoadOptions loadOptions = new TeXLoadOptions();
        using (Document pdfDoc = new Document(texFile, loadOptions))
        {
            // Save the document as PDF – equations and formatting are preserved.
            pdfDoc.Save(pdfFile);
        }

        Console.WriteLine($"Converted '{texFile}' to PDF '{pdfFile}'.");
    }
}