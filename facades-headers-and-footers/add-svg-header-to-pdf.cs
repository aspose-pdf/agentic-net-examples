using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string headerSvgPath = "diagram.svg";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(headerSvgPath))
        {
            Console.Error.WriteLine($"Header SVG not found: {headerSvgPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Create an ImageStamp from the SVG using a FileStream
        using (FileStream svgStream = File.OpenRead(headerSvgPath))
        {
            ImageStamp headerStamp = new ImageStamp(svgStream)
            {
                // Center horizontally, place at the top of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                // Top margin of 25 points (YIndent works with Top alignment)
                YIndent = 25,
                // Place the stamp in front of existing content
                Background = false
            };

            // Add the stamp to every page of the PDF
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(headerStamp);
            }
        }

        // Save the stamped PDF to the output file
        pdfDocument.Save(outputPdfPath);
    }
}
