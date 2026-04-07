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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation ann = page.Annotations[idx];

                    // Process only button annotations
                    if (ann is ButtonField button)
                    {
                        // Assign a new Border with dashed style and 2‑point thickness
                        button.Border = new Border(button)
                        {
                            Style = BorderStyle.Dashed,
                            Width = 2
                        };
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}