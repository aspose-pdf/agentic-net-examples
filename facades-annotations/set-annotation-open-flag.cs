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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Iterate through each page and set the Open flag on supported annotations
        foreach (Page page in doc.Pages)
        {
            for (int i = 0; i < page.Annotations.Count; i++)
            {
                Annotation ann = page.Annotations[i];

                // The Open property is defined only on markup‑type annotations such as TextAnnotation or PopupAnnotation
                if (ann is TextAnnotation textAnn)
                {
                    textAnn.Open = true;
                }
                else if (ann is PopupAnnotation popupAnn)
                {
                    popupAnn.Open = true;
                }
                // Add other markup types here if they expose an Open property
            }
        }

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"All annotation Open flags set to true. Saved to '{outputPath}'.");
    }
}
