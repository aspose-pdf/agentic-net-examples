using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF with the Document class (PdfFileEditor does not expose DeleteAnnotations).
        using (Document pdfDoc = new Document(inputPath))
        {
            // Ensure the requested range is within the document page count.
            int startPage = Math.Max(3, 1);
            int endPage   = Math.Min(7, pdfDoc.Pages.Count);

            for (int pageNum = startPage; pageNum <= endPage; pageNum++)
            {
                // Manual removal of Link annotations because AnnotationCollection.DeleteAll(string) is not available.
                var annots = pdfDoc.Pages[pageNum].Annotations;
                for (int i = annots.Count; i >= 1; i--)
                {
                    if (annots[i] is LinkAnnotation)
                        annots.Delete(i);
                }
            }

            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Link annotations removed from pages 3-7. Saved to '{outputPath}'.");
    }
}
