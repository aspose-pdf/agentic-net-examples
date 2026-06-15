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
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only LinkAnnotation objects
                    if (ann is LinkAnnotation link)
                    {
                        // Set border color to red
                        link.Color = Aspose.Pdf.Color.Red;

                        // Create a Border object for the annotation and set line width to 3 points
                        link.Border = new Border(link) { Width = 3 };
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotations updated and saved to '{outputPath}'.");
    }
}