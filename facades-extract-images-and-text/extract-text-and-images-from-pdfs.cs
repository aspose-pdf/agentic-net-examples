using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point of the service
    static void Main(string[] args)
    {
        // Input and output directories are taken from environment variables.
        // If not set, default to typical Docker mount points.
        string inputDir  = Environment.GetEnvironmentVariable("PDF_INPUT_DIR")  ?? "/data";
        string outputDir = Environment.GetEnvironmentVariable("PDF_OUTPUT_DIR") ?? "/output";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Process each PDF file found in the input directory.
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string txtPath = Path.Combine(outputDir, fileNameWithoutExt + ".txt");

            // PdfExtractor implements IDisposable – wrap it in a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);

                // NOTE: Older versions of Aspose.Pdf.Facades do not expose the ExtractionOptions
                // property. The default extraction mode already provides raw text, so we can
                // simply call ExtractText() without configuring additional options.

                // Perform the text extraction.
                extractor.ExtractText();

                // Write the extracted text to a .txt file.
                using (FileStream outStream = new FileStream(txtPath, FileMode.Create, FileAccess.Write))
                {
                    extractor.GetText(outStream);
                }
            }

            Console.WriteLine($"Extracted text from '{pdfPath}' to '{txtPath}'.");
        }
    }
}
