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
                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through all annotations on the page (1‑based indexing)
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];

                        // Set the Open flag where the property exists
                        if (annotation is TextAnnotation textAnn)
                        {
                            textAnn.Open = true;
                        }
                        else if (annotation is PopupAnnotation popupAnn)
                        {
                            popupAnn.Open = true;
                        }
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
                Console.WriteLine($"All annotation Open flags set. Saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}