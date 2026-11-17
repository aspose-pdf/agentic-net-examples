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
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only button form fields (push button annotations)
                    if (annot is ButtonField button)
                    {
                        // Set a dashed border with a thickness of 2 points
                        button.Border = new Border(button)
                        {
                            Width = 2,
                            Style = BorderStyle.Dashed
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