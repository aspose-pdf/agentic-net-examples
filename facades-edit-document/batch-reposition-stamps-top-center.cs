using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // required by task specification

class BatchStampReposition
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder  = @"C:\Pdf\Input";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\Pdf\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over all pages (1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Find all stamp annotations on the current page
                        var stampAnnotations = page.Annotations
                                                   .OfType<StampAnnotation>()
                                                   .ToList();

                        foreach (StampAnnotation stamp in stampAnnotations)
                        {
                            // Current stamp size
                            double stampWidth  = stamp.Rect.URX - stamp.Rect.LLX;
                            double stampHeight = stamp.Rect.URY - stamp.Rect.LLY;

                            // Page dimensions
                            double pageWidth  = page.PageInfo.Width;
                            double pageHeight = page.PageInfo.Height;

                            // Desired top‑center position (10 units margin from top edge)
                            double llx = (pageWidth - stampWidth) / 2.0;
                            double lly = pageHeight - stampHeight - 10.0;
                            double urx = llx + stampWidth;
                            double ury = pageHeight - 10.0;

                            // Update the rectangle using the fully qualified Rectangle type
                            stamp.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                        }
                    }

                    // Save the modified PDF to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Repositioned stamps: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}