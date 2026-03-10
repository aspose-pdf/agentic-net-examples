using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load the OFD file using the appropriate load options.
        OfdLoadOptions loadOpts = new OfdLoadOptions();

        // Document implements IDisposable – wrap in using for deterministic cleanup.
        using (Document doc = new Document(inputOfd, loadOpts))
        {
            // Ensure the document contains at least one page (Aspose.Pdf uses 1‑based indexing).
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Get the first page.
            Page page = doc.Pages[1];

            // Define the rectangle that will host the caret annotation.
            // Constructor: left, bottom, right, top.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the caret annotation on the selected page.
            CaretAnnotation caret = new CaretAnnotation(page, rect)
            {
                // Set visual properties.
                Color = Aspose.Pdf.Color.Red,
                // Optional text shown in the annotation's popup.
                Contents = "Caret annotation example",
                // Use a valid enum value – None is the default and always available.
                Symbol = CaretSymbol.None
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(caret);

            // Save the modified document as PDF (OFD cannot be saved; PDF is the output format).
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Caret annotation added and saved to '{outputPdf}'.");
    }
}
