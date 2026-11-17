using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // needed for TextState

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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Prepare the new watermark text with the current date
                string newWatermark = $"Watermark - {DateTime.Now:yyyy-MM-dd}";

                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Collect existing WatermarkAnnotations to avoid modifying the collection while iterating
                    List<WatermarkAnnotation> watermarks = new List<WatermarkAnnotation>();
                    foreach (Annotation annot in page.Annotations)
                    {
                        if (annot is WatermarkAnnotation wm)
                            watermarks.Add(wm);
                    }

                    // Replace the text of each found WatermarkAnnotation
                    foreach (WatermarkAnnotation wm in watermarks)
                    {
                        // Create a TextState (optional: customize appearance)
                        TextState ts = new TextState();
                        // Example customisation – you can adjust as needed
                        ts.FontSize = 36;
                        ts.ForegroundColor = Color.FromRgb(150, 150, 150);

                        // Set the new text using the correct API
                        wm.SetTextAndState(new string[] { newWatermark }, ts);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarks updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
