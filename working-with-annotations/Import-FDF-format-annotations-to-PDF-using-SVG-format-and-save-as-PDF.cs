using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and FDF files
        const string inputPdfPath = "input.pdf";
        const string fdfPath      = "annotations.fdf";

        // Temporary SVG file (intermediate format)
        const string tempSvgPath  = "temp.svg";

        // Final output PDF with imported annotations
        const string outputPdfPath = "output.pdf";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the original PDF document
            // -------------------------------------------------
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // -------------------------------------------------
                // 2. Import annotations from the FDF file
                // -------------------------------------------------
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // FdfReader reads annotations and adds them to the document
                    FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                }

                // -------------------------------------------------
                // 3. Export the annotated PDF to SVG (intermediate step)
                // -------------------------------------------------
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                pdfDoc.Save(tempSvgPath, svgOptions);
            }

            // -------------------------------------------------
            // 4. Load the generated SVG back as a PDF document
            // -------------------------------------------------
            // Aspose.Pdf can load SVG using SvgLoadOptions
            using (Document svgDoc = new Document(tempSvgPath, new SvgLoadOptions()))
            {
                // -------------------------------------------------
                // 5. Save the final PDF with the imported annotations
                // -------------------------------------------------
                svgDoc.Save(outputPdfPath);
            }

            // Optional: clean up the temporary SVG file
            if (File.Exists(tempSvgPath))
            {
                File.Delete(tempSvgPath);
            }

            Console.WriteLine($"Annotations imported and PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}