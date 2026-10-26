using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_sound.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Collect SoundAnnotations to delete (cannot modify collection while iterating)
                    var toDelete = new List<Annotation>();
                    foreach (Annotation annot in page.Annotations)
                    {
                        if (annot is SoundAnnotation)
                        {
                            toDelete.Add(annot);
                        }
                    }

                    // Delete the collected annotations
                    foreach (Annotation annot in toDelete)
                    {
                        page.Annotations.Delete(annot);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All SoundAnnotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
