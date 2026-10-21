using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Directory containing PDFs
        const string inputDirectory = @"C:\PdfFolder";
        // Optional: output directory (overwrites original files)
        const string outputDirectory = @"C:\PdfFolder\Processed";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the directory
        foreach (string filePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(filePath))
                {
                    // Iterate through all pages (1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                    {
                        Page page = pdfDoc.Pages[pageIndex];

                        // Collect stamp annotations to remove
                        var stampsToRemove = new System.Collections.Generic.List<StampAnnotation>();

                        foreach (Annotation annotation in page.Annotations)
                        {
                            if (annotation is StampAnnotation stamp)
                            {
                                stampsToRemove.Add(stamp);
                            }
                        }

                        // Remove collected stamp annotations
                        foreach (StampAnnotation stamp in stampsToRemove)
                        {
                            page.Annotations.Delete(stamp);
                        }
                    }

                    // Save the modified PDF (overwrite or place in output folder)
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(filePath));
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch stamp annotation removal completed.");
    }
}