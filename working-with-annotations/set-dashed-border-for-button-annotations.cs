using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page (1‑based indexing)
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation ann = page.Annotations[idx];

                    // Process only button fields
                    if (ann is ButtonField button)
                    {
                        // Ensure a Border object exists; create one if necessary
                        if (button.Border == null)
                        {
                            button.Border = new Border(button);
                        }

                        // Set the border style to dashed and thickness to 2 points
                        button.Border.Style = Aspose.Pdf.Annotations.BorderStyle.Dashed;
                        button.Border.Width = 2;

                        // Optional: define a dash pattern (on length, off length)
                        button.Border.Dash = new Dash(3, 3);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation borders updated and saved to '{outputPath}'.");
    }
}