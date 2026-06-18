using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchProcess
{
    static void Main()
    {
        const string inputFolder  = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // ---------- Remove all attachments ----------
                using (PdfContentEditor contentEditor = new PdfContentEditor())
                {
                    contentEditor.BindPdf(inputPath);
                    contentEditor.DeleteAttachments();               // rule: DeleteAttachments()
                    contentEditor.Save(outputPath);                  // rule: Save(string)
                }

                // ---------- Add standardized disclaimer annotation ----------
                using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
                {
                    annotEditor.BindPdf(outputPath);

                    // Define annotation rectangle (coordinates: llx, lly, urx, ury)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 500, 800);

                    // Create a free‑text (Text) annotation on the first page
                    TextAnnotation disclaimer = new TextAnnotation(annotEditor.Document.Pages[1], rect)
                    {
                        Title    = "Disclaimer",
                        Contents = "This document is for informational purposes only."
                    };

                    // Attach the annotation to page 1 (range 1‑1)
                    annotEditor.ModifyAnnotations(1, 1, disclaimer); // rule: ModifyAnnotations

                    annotEditor.Save(outputPath); // overwrite the same file with the annotation added
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}