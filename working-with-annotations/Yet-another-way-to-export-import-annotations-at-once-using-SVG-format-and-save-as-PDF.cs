using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_annotations.pdf";
        const string outputSvgPath = "output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Export all annotations to an in‑memory XFDF stream
            // ------------------------------------------------------------
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // rewind for later import

                // ------------------------------------------------------------
                // 2. Save the whole document (including annotations) as SVG
                // ------------------------------------------------------------
                // The SVG export lives in the Aspose.Pdf.Svg assembly. We try to load it
                // via reflection so the project can compile even when the package is not
                // referenced. If the assembly is missing we simply skip the SVG step – the
                // rest of the workflow (XFDF export/import) still works.
                const string svgOptionsTypeName = "Aspose.Pdf.Svg.SvgSaveOptions, Aspose.Pdf.Svg";
                try
                {
                    Type svgOptionsType = Type.GetType(svgOptionsTypeName, throwOnError: true);
                    // SvgSaveOptions derives from Aspose.Pdf.SaveOptions, so we can treat it as such.
                    SaveOptions svgOptions = (SaveOptions)Activator.CreateInstance(svgOptionsType);
                    pdfDoc.Save(outputSvgPath, svgOptions);
                    Console.WriteLine($"PDF saved as SVG: {outputSvgPath}");
                }
                catch (Exception ex) when (ex is TypeLoadException || ex is FileNotFoundException)
                {
                    // Assembly not found – inform the user and continue.
                    Console.WriteLine("Aspose.Pdf.Svg assembly not found. Skipping SVG export.");
                }

                // ------------------------------------------------------------
                // 3. Import the previously exported annotations back into the PDF
                // ------------------------------------------------------------
                pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);
                Console.WriteLine("Annotations re‑imported from XFDF.");
            }

            // ------------------------------------------------------------
            // 4. Save the final PDF (annotations are now present)
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Final PDF with annotations saved: {outputPdfPath}");
        }
    }
}
