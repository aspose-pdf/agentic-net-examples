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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Examine each annotation on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only button annotations (form button fields)
                    if (annot is ButtonField button)
                    {
                        // Border must be created with the parent annotation instance
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

        Console.WriteLine($"Button annotation borders updated and saved to '{outputPath}'.");
    }
}
