using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "InkWidthIncreased.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indices are 1‑based; get page 3
            Page page = doc.Pages[3];

            // Iterate over all annotations on the page
            foreach (Annotation annot in page.Annotations)
            {
                // Process only InkAnnotations
                if (annot is InkAnnotation inkAnnot)
                {
                    // Ensure the Border object exists – Border requires the owning annotation
                    if (inkAnnot.Border == null)
                        inkAnnot.Border = new Border(inkAnnot);

                    // Set the border width to 3 points
                    inkAnnot.Border.Width = 3;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}
