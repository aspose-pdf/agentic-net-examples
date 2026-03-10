using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPclPath  = "input.pcl";   // PCL file to be loaded
        const string outputPdfPath = "output.pdf";  // Resulting PDF file

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"File not found: {inputPclPath}");
            return;
        }

        // Load the PCL file (PCL is input‑only). No PclSaveOptions exist.
        using (Document doc = new Document(inputPclPath, new PclLoadOptions()))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Create a rectangle for the caret annotation.
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle caretRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the caret annotation on the first page.
            CaretAnnotation caret = new CaretAnnotation(doc.Pages[1], caretRect)
            {
                // Optional properties
                Contents = "Sample caret annotation",
                Color    = Aspose.Pdf.Color.Red,
                Symbol   = CaretSymbol.Paragraph   // associate a paragraph symbol
            };

            // Add the annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(caret);

            // Save the modified document as PDF (PCL cannot be used as an output format).
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Caret annotation added and saved to '{outputPdfPath}'.");
    }
}