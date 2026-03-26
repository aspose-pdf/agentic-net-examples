using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cloned_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Get the first page (pages are 1‑based)
                Page page = doc.Pages[1];

                // Ensure the page contains at least one annotation
                if (page.Annotations.Count > 0)
                {
                    // Retrieve the first annotation on the page
                    Annotation originalAnnotation = page.Annotations[1];

                    // Clone the annotation – Clone returns object, so cast to Annotation
                    Annotation clonedAnnotation = originalAnnotation.Clone() as Annotation;

                    if (clonedAnnotation != null)
                    {
                        // Modify desired properties of the cloned annotation
                        clonedAnnotation.Color = Aspose.Pdf.Color.Red;
                        clonedAnnotation.Contents = "This is a cloned annotation";

                        // Title exists only on markup‑type annotations, so cast before setting it
                        if (clonedAnnotation is MarkupAnnotation markup)
                        {
                            markup.Title = "Cloned Annotation";
                        }
                        else
                        {
                            Console.Error.WriteLine("Cloned annotation is not a markup annotation; Title cannot be set.");
                        }

                        // Add the cloned annotation back to the same page
                        page.Annotations.Add(clonedAnnotation);
                    }
                    else
                    {
                        Console.Error.WriteLine("Clone() returned null – cloning not supported for this annotation type.");
                    }
                }
                else
                {
                    Console.Error.WriteLine("No annotations found on the first page to clone.");
                }

                // Save the modified document
                doc.Save(outputPath);
                Console.WriteLine($"Document saved as '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
