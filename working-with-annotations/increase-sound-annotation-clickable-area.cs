using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_sound_adjusted.pdf";

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
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Iterate over all annotations on the page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Process only SoundAnnotation instances
                        if (annot is SoundAnnotation soundAnnot)
                        {
                            // Use the correct property 'Rect' for annotation bounds
                            Aspose.Pdf.Rectangle rect = soundAnnot.Rect;

                            double width = rect.URX - rect.LLX;
                            double height = rect.URY - rect.LLY;

                            // Increase clickable area by 20%
                            double newWidth = width * 1.20;
                            double newHeight = height * 1.20;

                            double deltaW = (newWidth - width) / 2.0;
                            double deltaH = (newHeight - height) / 2.0;

                            // Create a new rectangle centered on the original
                            Aspose.Pdf.Rectangle enlarged = new Aspose.Pdf.Rectangle(
                                rect.LLX - deltaW,
                                rect.LLY - deltaH,
                                rect.URX + deltaW,
                                rect.URY + deltaH);

                            // Apply the enlarged rectangle back to the annotation
                            soundAnnot.Rect = enlarged;
                        }
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Adjusted SoundAnnotations saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
