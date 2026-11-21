using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in doc.Pages)
                {
                    // Iterate through all annotations on the page
                    for (int i = 0; i < page.Annotations.Count; i++)
                    {
                        // Process only SoundAnnotation objects
                        if (page.Annotations[i] is SoundAnnotation soundAnn)
                        {
                            // Current rectangle of the annotation
                            Aspose.Pdf.Rectangle rect = soundAnn.Rect;

                            double llx = rect.LLX;
                            double lly = rect.LLY;
                            double urx = rect.URX;
                            double ury = rect.URY;

                            double width  = urx - llx;
                            double height = ury - lly;
                            double centerX = llx + width  / 2.0;
                            double centerY = lly + height / 2.0;

                            // Increase size by 20%
                            double newWidth  = width  * 1.20;
                            double newHeight = height * 1.20;

                            double newLLX = centerX - newWidth  / 2.0;
                            double newLLY = centerY - newHeight / 2.0;
                            double newURX = centerX + newWidth  / 2.0;
                            double newURY = centerY + newHeight / 2.0;

                            // Assign the enlarged rectangle back to the annotation
                            soundAnn.Rect = new Aspose.Pdf.Rectangle(newLLX, newLLY, newURX, newURY);
                        }
                    }
                }

                // Save the modified PDF
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