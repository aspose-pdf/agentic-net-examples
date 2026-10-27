using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                var testRectangles = new[]
                {
                    new { LLX = -10.0, LLY = 100.0, URX = 200.0, URY = 300.0 }, // left out of bounds
                    new { LLX = 50.0,  LLY = 50.0,  URX = 600.0, URY = 800.0 }, // may exceed page size
                    new { LLX = 100.0, LLY = 100.0, URX = 200.0, URY = 200.0 }  // valid rectangle
                };

                foreach (var rc in testRectangles)
                {
                    // Aspose.Pdf.Rectangle expects double values; use it for annotation bounds.
                    var annotRect = new Aspose.Pdf.Rectangle(rc.LLX, rc.LLY, rc.URX, rc.URY);

                    // Use SquareAnnotation – the correct annotation type for rectangular shapes.
                    var rectAnnot = new SquareAnnotation(page, annotRect);

                    try
                    {
                        // Adding the annotation may throw if coordinates are outside page bounds.
                        page.Annotations.Add(rectAnnot);
                    }
                    catch (PdfException ex) // bounds‑checking exception from Aspose.Pdf
                    {
                        Console.WriteLine($"Page {i}: Shape out of bounds – LLX={rc.LLX}, LLY={rc.LLY}, URX={rc.URX}, URY={rc.URY}. Exception: {ex.Message}");
                    }
                    catch (ArgumentException ex) // generic argument error
                    {
                        Console.WriteLine($"Page {i}: Invalid shape coordinates – LLX={rc.LLX}, LLY={rc.LLY}, URX={rc.URX}, URY={rc.URY}. Exception: {ex.Message}");
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
