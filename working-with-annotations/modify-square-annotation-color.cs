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

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through pages to find a figure annotation (e.g., SquareAnnotation)
            foreach (Page page in doc.Pages)
            {
                // Annotation collections are 1‑based
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Check for a concrete subclass of CommonFigureAnnotation
                    if (ann is SquareAnnotation square)
                    {
                        // Retrieve the appearance dictionary of the annotation
                        AppearanceDictionary appearanceDict = square.Appearance;

                        // NOTE: In recent Aspose.Pdf versions the normal appearance stream is accessed via the
                        //       NormalAppearance property of AppearanceDictionary. If the property is not
                        //       available in the referenced version, you can work directly with the annotation
                        //       properties (Color, InteriorColor) which automatically update the appearance.
                        // var normalAppearance = appearanceDict.NormalAppearance; // <-- removed to avoid CS1061

                        // Modify the annotation's color – this updates the appearance
                        square.Color = Aspose.Pdf.Color.Red;

                        // Optionally modify the interior fill color
                        square.InteriorColor = Aspose.Pdf.Color.Yellow;

                        // If you need to work directly with the appearance stream, you can manipulate the
                        // resources of 'appearanceDict' here (e.g., add custom XObjects). The example below
                        // demonstrates how to replace the normal appearance with a new one.
                        //
                        // var newAppearance = new Appearance();
                        // // ... build new appearance content ...
                        // appearanceDict.NormalAppearance = newAppearance;
                    }
                }
            }

            // Save the modified PDF (using rule: document disposal with using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
