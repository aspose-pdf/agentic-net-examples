using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "clean_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF (using the documented lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based; iterate through each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    // Clear all annotations on the current page
                    page.Annotations.Clear(); // AnnotationCollection.Clear deletes all annotations
                }

                // Save the cleaned PDF (Save without options always writes PDF)
                doc.Save(outputPath);
            }

            Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}