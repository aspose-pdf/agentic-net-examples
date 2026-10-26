using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Represents a rectangle shape defined by its lower‑left (llx,lly) and upper‑right (urx,ury) coordinates.
    struct ShapeBounds
    {
        public double Llx;
        public double Lly;
        public double Urx;
        public double Ury;

        public ShapeBounds(double llx, double lly, double urx, double ury)
        {
            Llx = llx;
            Lly = lly;
            Urx = urx;
            Ury = ury;
        }

        public override string ToString()
        {
            return $"LLX={Llx}, LLY={Lly}, URX={Urx}, URY={Ury}";
        }
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Example list of shape bounds that we want to add to each page.
        // In a real scenario these could come from a data source or calculation.
        List<ShapeBounds> shapes = new List<ShapeBounds>
        {
            new ShapeBounds(50,  700, 200, 750),   // valid rectangle
            new ShapeBounds(300, 800, 250, 850),   // invalid: llx > urx (will trigger bounds checking)
            new ShapeBounds(-10, 100, 100, 200)    // invalid: negative coordinate (may trigger exception)
        };

        // Use a using block for deterministic disposal of the Document.
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                foreach (ShapeBounds bounds in shapes)
                {
                    try
                    {
                        // Fully qualified Rectangle to avoid ambiguity with System.Drawing.
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                            bounds.Llx, bounds.Lly, bounds.Urx, bounds.Ury);

                        // Add a simple square annotation to visualise the rectangle.
                        // The annotation constructor validates the rectangle bounds.
                        SquareAnnotation annotation = new SquareAnnotation(page, rect);
                        annotation.Color = Aspose.Pdf.Color.LightGray;
                        annotation.Opacity = 0.5;
                        page.Annotations.Add(annotation);
                    }
                    catch (Exception ex)
                    {
                        // Log detailed information about the failure.
                        Console.Error.WriteLine(
                            $"Error adding shape to page {i}: {bounds}. Exception: {ex.GetType().Name} - {ex.Message}");
                    }
                }
            }

            // Save the modified PDF. No PreSave() is required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}