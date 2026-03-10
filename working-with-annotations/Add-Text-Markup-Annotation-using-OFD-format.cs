using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputOfd   = "input.ofd";
        const string outputPdf  = "annotated_output.pdf";

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load OFD (input‑only format) and work with it as a PDF document
        using (Document doc = new Document(inputOfd, new OfdLoadOptions()))
        {
            // Choose the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create an underline text‑markup annotation on the page
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Blue,   // annotation color
                Contents = "Important note",        // popup text
                Title    = "Reviewer"               // title shown in the popup
                // The 'Open' property is not available for markup annotations like UnderlineAnnotation.
            };

            // Add the annotation to the page
            page.Annotations.Add(underline);

            // Save the result as PDF (OFD cannot be saved)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
    }
}
