using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_header.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the document and insert a blank page at position 3
            // -------------------------------------------------
            Document pdfDoc = new Document(inputPdf);
            // Insert a new blank page before page 3 (1‑based index)
            pdfDoc.Pages.Insert(3);

            // -------------------------------------------------
            // 2. Add a header text annotation on the newly inserted page
            // -------------------------------------------------
            // Aspose.Pdf uses a rectangle defined by lower‑left (llx,lly) and upper‑right (urx,ury)
            // The original example used a 500pt wide × 30pt high box starting 50pt from the left
            // and 800pt from the bottom. Convert that to the required coordinates:
            //   llx = 50
            //   lly = 800
            //   urx = 50 + 500 = 550
            //   ury = 800 + 30  = 830
            var headerRect = new Aspose.Pdf.Rectangle(50, 800, 550, 830);

            var headerAnnotation = new TextAnnotation(pdfDoc.Pages[3], headerRect)
            {
                Title    = "Header",
                Contents = "Document Header Text"
            };
            pdfDoc.Pages[3].Annotations.Add(headerAnnotation);

            // -------------------------------------------------
            // 3. Save the final PDF
            // -------------------------------------------------
            pdfDoc.Save(outputPdf);
            Console.WriteLine($"Successfully created PDF with a blank page and header annotation: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
