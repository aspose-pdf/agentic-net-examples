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

        // Load the document
        Document doc = new Document(inputPath);
        LineAnnotation lineAnnot = null;
        int pageNumber = 0;

        // Locate the first Line annotation in the document (1‑based page indexing)
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is LineAnnotation la)
                {
                    lineAnnot = la;
                    pageNumber = i;
                    break;
                }
            }
            if (lineAnnot != null) break;
        }

        if (lineAnnot == null)
        {
            Console.Error.WriteLine("No line annotation found in the PDF.");
            return;
        }

        // The Border class for annotations does not expose a DashArray property.
        // To create a dashed line we set the Border.Style to Dashed. The exact dash
        // pattern (e.g., 5‑2) cannot be customized via the public API in Aspose.PDF
        // 26.10.0, so we use the default dash pattern provided by the library.
        lineAnnot.Border = new Border(lineAnnot)
        {
            Width = 1,
            Style = BorderStyle.Dashed // results in a dashed line (default dash pattern)
        };

        // Save the modified document
        doc.Save(outputPath);
        Console.WriteLine($"Dash pattern updated and saved to '{outputPath}'.");
    }
}
