using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF with the Document class (PdfFileEditor does not support annotation modification).
            Document pdfDoc = new Document(inputPath);

            // Update the Modified date of every markup annotation to the current system time.
            DateTime now = DateTime.Now;
            foreach (Page page in pdfDoc.Pages)
            {
                if (page.Annotations == null) continue;
                foreach (Annotation annotation in page.Annotations)
                {
                    // Only markup annotations expose the Modified property.
                    if (annotation is MarkupAnnotation markup)
                    {
                        markup.Modified = now;
                    }
                }
            }

            // Save the updated PDF.
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Annotations' Modified date updated. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
