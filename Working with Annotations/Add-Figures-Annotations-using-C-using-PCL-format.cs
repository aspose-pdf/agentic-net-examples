using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPcl = "input.pcl";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPcl))
        {
            Console.Error.WriteLine($"File not found: {inputPcl}");
            return;
        }

        // Load the PCL file (PCL is input‑only). Use PclLoadOptions.
        using (Document doc = new Document(inputPcl, new PclLoadOptions()))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were loaded from the PCL file.");
                return;
            }

            // Work with the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // ----- Add a Circle (figure) annotation -----
            // Rectangle defines the bounding box of the circle.
            Aspose.Pdf.Rectangle circleRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color = Aspose.Pdf.Color.Red,               // Border color
                InteriorColor = Aspose.Pdf.Color.Yellow,   // Fill color
                Contents = "Sample circle figure annotation"
            };
            page.Annotations.Add(circle);

            // ----- Add a Square (figure) annotation -----
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(350, 500, 550, 700);
            SquareAnnotation square = new SquareAnnotation(page, squareRect)
            {
                Color = Aspose.Pdf.Color.Blue,
                InteriorColor = Aspose.Pdf.Color.LightGray,
                Contents = "Sample square figure annotation"
            };
            page.Annotations.Add(square);

            // Save the modified document as PDF.
            // PCL cannot be used as an output format (no PclSaveOptions).
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Figure annotations added and PDF saved to '{outputPdf}'.");
    }
}