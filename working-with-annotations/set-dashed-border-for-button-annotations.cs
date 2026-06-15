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
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only button fields
                    if (ann is ButtonField button)
                    {
                        // Ensure a Border object exists
                        if (button.Border == null)
                            button.Border = new Border(button);

                        // Set border style to dashed and thickness to 2 points
                        button.Border.Style = BorderStyle.Dashed;
                        button.Border.Width = 2;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button borders updated and saved to '{outputPath}'.");
    }
}