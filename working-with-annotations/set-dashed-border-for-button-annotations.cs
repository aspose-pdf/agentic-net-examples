using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // Border, BorderStyle
using Aspose.Pdf.Forms;        // ButtonField

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation ann = page.Annotations[idx];

                    // Process only button fields
                    if (ann is ButtonField button)
                    {
                        // Create a Border linked to this annotation and set style & thickness
                        button.Border = new Border(button)
                        {
                            Style = BorderStyle.Dashed, // Dashed border
                            Width = 2                  // Thickness of two points
                        };
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use provided save logic)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All button annotations updated and saved to '{outputPath}'.");
    }
}