using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a StrikeOut (strikethrough) annotation and set its properties
            StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect)
            {
                // Use Subject to store the author information (StrikeOutAnnotation inherits from MarkupAnnotation)
                Subject = "Jane Smith",

                // Optional: set a comment that appears in the annotation popup
                Contents = "This text is struck through."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation added with author 'Jane Smith'. Saved to '{outputPath}'.");
    }
}
