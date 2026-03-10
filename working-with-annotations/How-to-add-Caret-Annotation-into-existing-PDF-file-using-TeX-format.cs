using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source TeX file and the resulting PDF.
        const string texFile   = "input.tex";
        const string outputPdf = "output.pdf";

        // Verify that the TeX source exists.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Load the TeX file and convert it to a PDF document.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document doc = new Document(texFile, texLoadOptions))
        {
            // Ensure the document contains at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The generated PDF has no pages.");
                return;
            }

            // Define the rectangle (in points) where the caret annotation will appear.
            // Fully qualified type is used to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle caretRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a CaretAnnotation on the first page.
            CaretAnnotation caret = new CaretAnnotation(doc.Pages[1], caretRect)
            {
                // Optional visual properties.
                Color    = Aspose.Pdf.Color.Red,
                Contents = "Sample caret annotation"
                // Symbol can be set if needed, e.g. caret.Symbol = "Insert";
            };

            // Add the annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(caret);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with caret annotation saved to '{outputPdf}'.");
    }
}