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
        const string targetAnnotationName = "Annot123"; // Name of the annotation to modify

        // New rectangle coordinates (lower‑left X/Y and upper‑right X/Y)
        const double llx = 100;
        const double lly = 500;
        const double urx = 300;
        const double ury = 550;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            bool found = false;

            // Search all pages for the annotation with the specified Name
            foreach (Page page in doc.Pages)
            {
                // Preferred way: FindByName returns the annotation directly (or null)
                Annotation annotation = page.Annotations.FindByName(targetAnnotationName);

                // Fallback: enumerate annotations and compare the Name property
                if (annotation == null)
                {
                    foreach (Annotation ann in page.Annotations)
                    {
                        if (!string.IsNullOrEmpty(ann.Name) &&
                            ann.Name.Equals(targetAnnotationName, StringComparison.Ordinal))
                        {
                            annotation = ann;
                            break;
                        }
                    }
                }

                if (annotation != null)
                {
                    // Update the annotation rectangle
                    annotation.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                    found = true;
                    break; // Assuming Names are unique across the document
                }
            }

            if (!found)
            {
                Console.Error.WriteLine($"Annotation with Name '{targetAnnotationName}' not found.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation rectangle updated and saved to '{outputPath}'.");
    }
}
