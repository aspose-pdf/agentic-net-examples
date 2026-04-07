using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through pages to find a figure annotation (e.g., SquareAnnotation)
            foreach (Page page in doc.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Check if the annotation is a concrete figure type
                    if (ann is SquareAnnotation square)
                    {
                        // Retrieve the appearance dictionary (appearance stream container)
                        AppearanceDictionary appearance = square.Appearance;

                        // Example: access the normal appearance stream (if needed)
                        // var normalAppearance = appearance.Normal; // XObject/FormXObject

                        // Modify the annotation's color – this updates its appearance stream
                        square.Color = Aspose.Pdf.Color.Red;

                        // Optionally, also change the interior fill color
                        square.InteriorColor = Aspose.Pdf.Color.Yellow;

                        // If you need to force regeneration of the appearance stream:
                        // square.UpdateAppearance();

                        Console.WriteLine($"Modified color of a SquareAnnotation on page {page.Number}");
                    }
                    else if (ann is CircleAnnotation circle)
                    {
                        // Retrieve appearance dictionary
                        AppearanceDictionary appearance = circle.Appearance;

                        // Change color
                        circle.Color = Aspose.Pdf.Color.Green;
                        circle.InteriorColor = Aspose.Pdf.Color.LightGray;

                        Console.WriteLine($"Modified color of a CircleAnnotation on page {page.Number}");
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
