using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "LatexOutput";
        // Path.Combine cannot be used in a const declaration because it is evaluated at runtime.
        string outputPath = Path.Combine(outputDir, "output.tex");

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Use TeXSaveOptions for LaTeX conversion. The OutputDirectory property does not exist on this class,
                // so auxiliary files will be written to the same folder as the output .tex file by default.
                TeXSaveOptions texOpts = new TeXSaveOptions();

                // Save the document as LaTeX (TeX) source
                doc.Save(outputPath, texOpts);
            }

            Console.WriteLine($"LaTeX conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            // Generate a detailed crash report using the recommended constructor pattern.
            var crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                CrashReportFilename = "crash_report.html",
                CustomMessage = "Unexpected error during PDF to LaTeX conversion."
            };
            PdfException.GenerateCrashReport(crashOptions);

            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
