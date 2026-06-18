using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // for low‑level PDF operators

#pragma warning disable NU1903 // suppress known‑vulnerability warning for Microsoft.Bcl.Memory

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string headerImg  = "header.png";

        if (!File.Exists(inputPdf) || !File.Exists(headerImg))
        {
            Console.Error.WriteLine("Input PDF or header image not found.");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Desired header image size (points). Adjust as needed.
            const double imgWidth  = 100;
            const double imgHeight = 50;

            // Add the header image to each page without overlapping existing content
            foreach (Page page in doc.Pages)
            {
                double pageHeight = page.PageInfo.Height;

                // ------------------------------------------------------------
                // 1. Shift existing page content down by the height of the header.
                //    This is done by inserting low‑level PDF operators at the
                //    beginning of the page content stream.
                // ------------------------------------------------------------
                OperatorCollection ops = page.Contents;
                // Save the current graphics state (q)
                ops.Insert(0, new GSave());
                // Apply a translation matrix that moves everything down
                // (cm 1 0 0 1 0 -imgHeight)
                ops.Insert(1, new ConcatenateMatrix(1, 0, 0, 1, 0, -imgHeight));
                // Restore the graphics state after the original content (Q)
                ops.Add(new GRestore());

                // ------------------------------------------------------------
                // 2. Add the header image at the top of the page.
                // ------------------------------------------------------------
                // Rectangle positioned at the top‑left corner of the page.
                // (llx, lly) = (0, pageHeight - imgHeight)
                // (urx, ury) = (imgWidth, pageHeight)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,                     // lower‑left X
                    pageHeight - imgHeight, // lower‑left Y
                    imgWidth,               // upper‑right X
                    pageHeight);            // upper‑right Y

                // Add the image; it will be placed inside the rectangle.
                page.AddImage(headerImg, rect);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header image added to each page: {outputPdf}");
    }
}
