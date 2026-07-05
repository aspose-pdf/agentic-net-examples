using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Annotations collection is also 1‑based
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Process only button fields
                    if (ann is ButtonField button)
                    {
                        // Create a Border linked to the button annotation
                        Border border = new Border(button);
                        border.Style = BorderStyle.Dashed; // Dashed border style
                        border.Width = 2;                  // Thickness of 2 points
                        // Optional dash pattern (on 3, off 3)
                        border.Dash = new Dash(3, 3);

                        // Assign the configured border back to the button
                        button.Border = border;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}