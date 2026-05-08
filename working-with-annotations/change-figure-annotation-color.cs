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

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Locate the first figure annotation (SquareAnnotation, CircleAnnotation, etc.)
            CommonFigureAnnotation figure = null;

            foreach (Page page in doc.Pages)
            {
                // Annotation collections are 1‑based
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is CommonFigureAnnotation cf)
                    {
                        figure = cf;
                        break;
                    }
                }
                if (figure != null) break;
            }

            if (figure != null)
            {
                // Directly modify the annotation's color. This automatically updates its appearance.
                figure.Color = Aspose.Pdf.Color.Red;

                // Ensure the appearance is regenerated before saving (static rule)
                Annotation.UpdateAppearanceOnConvert = true;
            }
            else
            {
                Console.WriteLine("No figure annotation found in the document.");
            }

            // Save the modified PDF (lifecycle rule: using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
