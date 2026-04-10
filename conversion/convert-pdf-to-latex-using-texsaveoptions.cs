using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPdfPath = "sample.pdf";

        // Desired output directory for the LaTeX files.
        const string outputDirectory = "LaTeXOutput";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Full path for the generated .tex file.
        string outputTexPath = Path.Combine(outputDirectory, "sample.tex");

        // Verify that the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Initialize TeX (LaTeX) save options.
                Aspose.Pdf.TeXSaveOptions texOptions = new Aspose.Pdf.TeXSaveOptions();

                // Specify the directory where auxiliary files will be written.
                texOptions.OutDirectoryPath = outputDirectory;

                // Save the PDF as a LaTeX (.tex) file using the specified options.
                pdfDocument.Save(outputTexPath, texOptions);
            }

            Console.WriteLine($"LaTeX file successfully saved to '{outputTexPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}