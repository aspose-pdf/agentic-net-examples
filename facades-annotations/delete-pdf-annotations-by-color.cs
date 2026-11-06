using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Target RGB color to match (example: pure red)
        const int targetR = 255;
        const int targetG = 0;
        const int targetB = 0;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document to enumerate its annotations.
        Document pdfDoc = new Document(inputPath);

        // Aspose.Pdf.Color does not have GetRGBColor. Use FromRgb with normalized (0‑1) values.
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromRgb(
            targetR / 255.0,
            targetG / 255.0,
            targetB / 255.0);

        // PdfAnnotationEditor works with annotations without needing a Document instance for deletion.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Iterate through all pages and their annotations.
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // Some annotation types may not have a Color (e.g., Link). Guard against null.
                    if (annot.Color != null && annot.Color.Equals(targetColor))
                    {
                        // Delete the annotation whose color matches the target RGB value.
                        // Use the annotation's Name property (Id does not exist on Annotation).
                        if (!string.IsNullOrEmpty(annot.Name))
                        {
                            editor.DeleteAnnotation(annot.Name);
                        }
                    }
                }
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations with the specified color removed. Saved to '{outputPath}'.");
    }
}
