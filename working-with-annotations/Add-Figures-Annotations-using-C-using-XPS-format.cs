using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output files
        const string outputPdf = "figure_annotation.pdf";
        const string outputXps = "figure_annotation.xps";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the square annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a square (figure) annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,               // Border color
                InteriorColor = Aspose.Pdf.Color.LightGray, // Fill color
                Contents = "Sample square figure annotation",
                Title = "Figure 1"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the PDF (optional, shows the result before XPS conversion)
            doc.Save(outputPdf);

            // Prepare XPS save options (XpsSaveOptions lives in Aspose.Pdf namespace)
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // XPS conversion relies on GDI+ which is only available on Windows.
            // Guard the call with a platform check to avoid the System.Drawing GDI+ initialization error on non‑Windows OS.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputXps, xpsOptions);
                Console.WriteLine($"XPS version saved to '{outputXps}'.");
            }
            else
            {
                Console.WriteLine("XPS conversion is only supported on Windows platforms. Skipping XPS save.");
            }
        }

        Console.WriteLine($"PDF with figure annotation saved to '{outputPdf}'.");
    }
}