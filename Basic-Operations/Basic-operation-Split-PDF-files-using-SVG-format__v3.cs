using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SplitSvg";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Split the PDF into single‑page PDF streams
        PdfFileEditor pdfEditor = new PdfFileEditor();
        MemoryStream[] pageStreams = pdfEditor.SplitToPages(inputPdfPath);

        // Convert each single‑page PDF stream to an SVG file
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset stream position before loading
            pageStreams[i].Position = 0;

            // Load the single‑page PDF from the memory stream
            using (Document pageDoc = new Document(pageStreams[i]))
            {
                string svgFilePath = Path.Combine(outputFolder, $"Page_{i + 1}.svg");

                // Save as SVG using SvgSaveOptions (no Aspose.Pdf.Saving namespace needed)
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                pageDoc.Save(svgFilePath, svgOptions);

                Console.WriteLine($"Page {i + 1} saved as SVG → {svgFilePath}");
            }
        }

        Console.WriteLine("PDF split and SVG conversion completed.");
    }
}