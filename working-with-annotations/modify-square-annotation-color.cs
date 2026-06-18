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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Locate a figure annotation (e.g., SquareAnnotation) on the page
            SquareAnnotation figure = null;
            foreach (Annotation ann in page.Annotations)
            {
                if (ann is SquareAnnotation sq)
                {
                    figure = sq;
                    break;
                }
            }

            if (figure == null)
            {
                Console.WriteLine("No square figure annotation found on the first page.");
            }
            else
            {
                // Retrieve the appearance dictionary (for illustration)
                AppearanceDictionary appearance = figure.Appearance;

                // Modify the annotation's color – this updates its appearance stream
                figure.Color = Aspose.Pdf.Color.Red;

                // Ensure the appearance is refreshed (optional)
                figure.ChangeAfterResize(null);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}