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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one annotation on the first page
                if (doc.Pages[1].Annotations.Count == 0)
                {
                    Console.Error.WriteLine("No annotations found on the first page to clone.");
                    return;
                }

                // Get the first annotation from page 1 (cast from object to Annotation)
                Annotation originalAnnotation = doc.Pages[1].Annotations[1] as Annotation;
                if (originalAnnotation == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the original annotation.");
                    return;
                }

                // Clone the annotation (Clone returns object, so cast to Annotation)
                Annotation clonedAnnotation = originalAnnotation.Clone() as Annotation;
                if (clonedAnnotation == null)
                {
                    Console.Error.WriteLine("Clone operation returned null. Unable to clone the annotation.");
                    return;
                }

                // Change the color of the cloned annotation
                clonedAnnotation.Color = Aspose.Pdf.Color.Red;

                // Add the cloned annotation to page 2 (create page 2 if it does not exist)
                if (doc.Pages.Count < 2)
                {
                    doc.Pages.Add();
                }
                doc.Pages[2].Annotations.Add(clonedAnnotation);

                // Save the modified document
                doc.Save(outputPath);
                Console.WriteLine($"Cloned annotation saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
