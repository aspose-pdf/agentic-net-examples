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
        // Set to true to keep the visual look of annotations (flatten them) before removal.
        bool preserveAppearance = true;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    // Iterate backwards because we may delete items from the collection.
                    for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                    {
                        Annotation annotation = page.Annotations[annIndex];
                        if (preserveAppearance)
                        {
                            // Convert the annotation to a static graphic on the page.
                            annotation.Flatten();
                        }
                        else
                        {
                            // Directly delete the annotation without preserving its appearance.
                            page.Annotations.Delete(annIndex);
                        }
                    }
                }

                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}