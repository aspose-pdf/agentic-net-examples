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
        using (Document doc = new Document(inputPath))
        {
            // Iterate through every page
            foreach (Page page in doc.Pages)
            {
                // Iterate through every annotation on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Only TextAnnotation and PopupAnnotation expose the Open property
                    switch (ann)
                    {
                        case TextAnnotation textAnn:
                            textAnn.Open = true;
                            break;
                        case PopupAnnotation popupAnn:
                            popupAnn.Open = true;
                            break;
                        // If other annotation types gain an Open property in future, they can be added here
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All annotation Open flags set to true and saved to '{outputPath}'.");
    }
}
