using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page (1‑based indexing)
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // We're interested in figure annotations (e.g., SquareAnnotation or CircleAnnotation)
                    if (ann is SquareAnnotation square)
                    {
                        // Retrieve the appearance dictionary – it may be null if the annotation has no custom appearance
                        AppearanceDictionary appearance = square.Appearance;
                        bool hasAppearance = appearance != null;
                        Console.WriteLine($"Found SquareAnnotation on page {page.Number}, appearance exists: {hasAppearance}");

                        // Modify the annotation's border color (this updates the appearance automatically)
                        square.Color = Aspose.Pdf.Color.Blue;

                        // Optionally, modify the interior fill color as well
                        square.InteriorColor = Aspose.Pdf.Color.LightBlue;
                    }
                    else if (ann is CircleAnnotation circle)
                    {
                        // Same handling for CircleAnnotation
                        AppearanceDictionary appearance = circle.Appearance;
                        bool hasAppearance = appearance != null;
                        Console.WriteLine($"Found CircleAnnotation on page {page.Number}, appearance exists: {hasAppearance}");

                        circle.Color = Aspose.Pdf.Color.Green;
                        circle.InteriorColor = Aspose.Pdf.Color.LightGreen;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
