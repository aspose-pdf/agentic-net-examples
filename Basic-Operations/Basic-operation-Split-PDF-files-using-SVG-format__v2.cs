using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions, including SvgSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputSvg = "output.svg";   // Base name; multiple SVG files will be created

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Wrap the Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure SVG save options to generate one SVG file per page.
                // When TreatTargetFileNameAsDirectory is false (default), files are named:
                // output.svg, output_2.svg, output_3.svg, ...
                // Set to true if you prefer a separate folder named "output" to hold the files.
                SvgSaveOptions svgOpts = new SvgSaveOptions
                {
                    TreatTargetFileNameAsDirectory = false
                };

                // Save the document; Aspose.Pdf will emit separate SVG files for each page.
                doc.Save(outputSvg, svgOpts);
            }

            Console.WriteLine("PDF successfully split into SVG files.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}