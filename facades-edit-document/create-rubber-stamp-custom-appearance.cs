using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the custom appearance PDF and the output PDF
        const string inputPdf      = "input.pdf";
        const string appearancePdf = "customAppearance.pdf";
        const string outputPdf     = "output_custom_appearance.pdf";

        // Ensure the required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(appearancePdf))
        {
            Console.Error.WriteLine($"Custom appearance PDF not found: {appearancePdf}");
            return;
        }

        // PdfContentEditor is a facade that allows editing annotations.
        // It implements IDisposable via SaveableFacade, so we wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF to the editor.
            editor.BindPdf(inputPdf);

            // Open the custom appearance file as a stream.
            // The stream will be used as the appearance stream for the rubber‑stamp annotation.
            using (FileStream appearanceStream = File.OpenRead(appearancePdf))
            {
                // Define the rectangle (in points) where the annotation will be placed.
                // PdfContentEditor expects System.Drawing.Rectangle, so we fully qualify it.
                var annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

                // Create a rubber‑stamp annotation on page 1.
                // Parameters:
                //   page number (1‑based),
                //   annotation rectangle,
                //   contents (tooltip text),
                //   color of the annotation border,
                //   stream containing the custom appearance.
                editor.CreateRubberStamp(
                    page: 1,
                    annotRect: annotRect,
                    annotContents: "Custom Graphic Annotation",
                    color: System.Drawing.Color.Black,
                    appearanceStream: appearanceStream);
            }

            // Save the modified PDF to the output file.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with custom appearance saved to '{outputPdf}'.");
    }
}