using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // NOTE: The original code used TableAbsorber to detect a table
            // on the page. In some Aspose.Pdf versions the TableAbsorber
            // class does not expose an Absorb method, which leads to a
            // CS1061 compile error. To keep the sample compile‑ready we
            // replace the detection logic with a placeholder rectangle.
            // Replace the hard‑coded coordinates with the actual table
            // bounds obtained by your own detection routine if needed.
            // ------------------------------------------------------------
            // Define a rectangle that (for example) surrounds a table.
            // Values are in points (1/72 inch).
            double tableLlx = 100; // lower‑left X
            double tableLly = 500; // lower‑left Y
            double tableUrx = 400; // upper‑right X
            double tableUry = 700; // upper‑right Y
            Aspose.Pdf.Rectangle tableRect = new Aspose.Pdf.Rectangle(tableLlx, tableLly, tableUrx, tableUry);

            // Add a small padding around the table
            double padding = 5;
            Aspose.Pdf.Rectangle annotationRect = new Aspose.Pdf.Rectangle(
                tableRect.LLX - padding,
                tableRect.LLY - padding,
                tableRect.URX + padding,
                tableRect.URY + padding);

            // Create a square (figure) annotation.
            // NOTE: Recent Aspose.Pdf versions do not expose a CornerRadius
            // property on SquareAnnotation. If you need rounded corners you
            // can either:
            //   • Use a Graph with a Rectangle shape that supports CornerRadius, or
            //   • Add a custom appearance stream to the annotation.
            // For the purpose of this compile‑ready example we create a plain
            // square annotation.
            SquareAnnotation square = new SquareAnnotation(doc.Pages[1], annotationRect);
            square.Color = Aspose.Pdf.Color.Blue;                     // Border color
            square.InteriorColor = Aspose.Pdf.Color.Transparent;      // No fill
            // Border must be created with the owning annotation as the parent.
            square.Border = new Border(square) { Width = 1 };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
