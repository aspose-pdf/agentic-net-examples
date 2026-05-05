using System;
using System.Drawing; // System.Drawing.Rectangle and System.Drawing.Color are required by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and edit it with PdfContentEditor
        using (Document doc = new Document(inputPath))
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the editor to the loaded document
            editor.BindPdf(doc);

            // Define the rectangle area for the annotation (System.Drawing.Rectangle)
            // Parameters: x, y, width, height
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a square (rectangle) annotation on page 1
            // contents: optional text, color: blue, square:true, page:1, borderWidth:2
            editor.CreateSquareCircle(annotRect, "Aspose.Pdf.Rectangle Annotation", System.Drawing.Color.Blue, true, 1, 2);

            // Retrieve the newly added annotation (it will be the last one on the page)
            Page firstPage = doc.Pages[1];
            // Annotations collection is zero‑based, so use Count‑1
            Annotation lastAnnotation = firstPage.Annotations[firstPage.Annotations.Count - 1];

            // The created annotation is a SquareAnnotation; cast to access specific members
            if (lastAnnotation is SquareAnnotation squareAnnot)
            {
                // Set 75% opacity (available on SquareAnnotation)
                squareAnnot.Opacity = 0.75f;

                // Configure a custom dash pattern (e.g., 3 units on, 2 units off)
                // Border constructor requires the parent annotation instance
                squareAnnot.Border = new Border(squareAnnot)
                {
                    Width = 2,
                    Dash = new Dash(new int[] { 3, 2 })
                };
            }

            // Save the modified PDF using the editor (or the document directly)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aspose.Pdf.Rectangle annotation added and saved to '{outputPath}'.");
    }
}
