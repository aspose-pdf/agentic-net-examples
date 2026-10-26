using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string tempSvgPath   = "temp.svg";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine("Source or target PDF file not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Extract the vector graphic from the first page of the source PDF
        //         by saving that page as an SVG (vector format).
        // -----------------------------------------------------------------
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Save the whole document as SVG – the example focuses on the first page.
            // If only the first page is required, the SVG can be trimmed later.
            var svgOpts = new SvgSaveOptions();
            srcDoc.Save(tempSvgPath, svgOpts);
        }

        // -----------------------------------------------------------------
        // Step 2 & 3: Load the generated SVG as an Image and insert it into the
        //             Paragraphs collection of the first page of the target PDF.
        // -----------------------------------------------------------------
        using (Document tgtDoc = new Document(targetPdfPath))
        {
            Page firstPage = tgtDoc.Pages[1];

            // The SVG file is read as a stream and assigned to the Image.ImageStream
            // property. This avoids constructors that are not available in the current
            // Aspose.Pdf version.
            using (FileStream svgStream = new FileStream(tempSvgPath, FileMode.Open, FileAccess.Read))
            {
                Image pdfImage = new Image
                {
                    ImageStream = svgStream,
                    // Optional: set desired dimensions (points). Adjust as needed.
                    FixWidth  = 200,
                    FixHeight = 200,
                    // You can also control alignment instead of absolute positioning.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                firstPage.Paragraphs.Add(pdfImage);

                // Save the modified PDF while the stream is still open.
                tgtDoc.Save(outputPdfPath);
            }
        }

        // Clean up the temporary SVG file
        try
        {
            File.Delete(tempSvgPath);
        }
        catch
        {
            // Ignored – if deletion fails, the file will be left on disk.
        }

        Console.WriteLine($"Vector graphic inserted and saved to '{outputPdfPath}'.");
    }
}
