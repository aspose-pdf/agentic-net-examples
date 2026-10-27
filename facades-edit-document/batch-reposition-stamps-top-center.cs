using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchStampReposition
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for the modified PDFs
        const string outputFolder = @"C:\OutputPdfs";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Verify the PDF file exists before loading
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"File not found, skipping: {inputPath}");
                continue;
            }

            // Load the PDF with the core Document API (must be disposed via using)
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count;

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over a copy of the annotations collection to avoid modification issues
                    for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                    {
                        Annotation annotation = page.Annotations[annIndex];

                        // Only process stamp annotations
                        if (annotation is StampAnnotation stamp)
                        {
                            // Current stamp dimensions
                            double stampWidth = stamp.Rect.URX - stamp.Rect.LLX;
                            double stampHeight = stamp.Rect.URY - stamp.Rect.LLY;

                            // Page dimensions
                            double pageWidth = page.PageInfo.Width;
                            double pageHeight = page.PageInfo.Height;

                            // Compute new rectangle: top‑center of the page
                            double newLlx = (pageWidth - stampWidth) / 2.0;
                            double newLly = pageHeight - stampHeight; // top edge
                            double newUrx = newLlx + stampWidth;
                            double newUry = pageHeight;

                            // Apply the new position
                            stamp.Rect = new Aspose.Pdf.Rectangle(newLlx, newLly, newUrx, newUry);
                        }
                    }
                }

                // Save the modified PDF to the output folder
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));
                doc.Save(outputPath);
            }
        }

        Console.WriteLine("Batch stamp repositioning completed.");
    }
}
