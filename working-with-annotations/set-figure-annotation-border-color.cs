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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Check if the annotation is a figure annotation (inherits from CommonFigureAnnotation)
                    if (ann is CommonFigureAnnotation)
                    {
                        // Set the border color to dark gray (System.Drawing.Color is required for the Border property)
                        ann.Characteristics.Border = System.Drawing.Color.DarkGray;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All figure annotation borders updated and saved to '{outputPath}'.");
    }
}
