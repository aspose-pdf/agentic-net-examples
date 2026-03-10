using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the directory of the executing assembly.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        // Input TeX file and output PDF file (both placed in the same folder as the executable).
        string texFile = Path.Combine(baseDir, "sample.tex");
        string pdfFile = Path.Combine(baseDir, "output.pdf");

        // Verify that the TeX source exists before attempting to load it.
        if (!File.Exists(texFile))
        {
            Console.WriteLine($"TeX source file not found: '{texFile}'. Please ensure the file exists.");
            return;
        }

        // Initialize TeX load options (default settings).
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX file and generate a PDF document.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Configure PDF metadata.
            pdfDocument.Info.Title    = "My Document Title";
            pdfDocument.Info.Author   = "John Doe";
            pdfDocument.Info.Subject  = "Sample Subject";
            pdfDocument.Info.Keywords = "keyword1, keyword2";

            // Save the PDF with the metadata applied.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"PDF generated with metadata at '{pdfFile}'.");
    }
}
