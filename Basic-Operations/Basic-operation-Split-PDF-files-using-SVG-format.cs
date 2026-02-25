using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, SvgSaveOptions, etc.)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Source PDF file
        const string outputSvgPath = "output.svg";         // Base name for SVG output

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure SVG save options.
                // By default, saving a multi‑page PDF to SVG creates one SVG per page:
                // output.svg, output_2.svg, output_3.svg, ...
                // The TreatTargetFileNameAsDirectory flag can be set to true if you prefer
                // the files to be placed in a dedicated folder (optional).
                SvgSaveOptions svgOptions = new SvgSaveOptions
                {
                    // Uncomment the next line to place each page SVG into a folder named "output"
                    // TreatTargetFileNameAsDirectory = true
                };

                // Save the entire document as SVG. Multiple SVG files will be generated,
                // one for each page of the source PDF.
                pdfDoc.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine("PDF successfully split into SVG files.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}