using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "Comment1";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the Document class (PdfFileEditor does not provide DeleteAnnotation).
        using (Document pdfDoc = new Document(inputPath))
        {
            bool annotationDeleted = false;

            // Iterate through all pages and their annotations to find the one with the specified name.
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate backwards so we can safely remove items while iterating.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    var annotation = page.Annotations[i];
                    if (annotation.Name == annotationName)
                    {
                        page.Annotations.Delete(i);
                        annotationDeleted = true;
                        break; // Assuming only one annotation with this name.
                    }
                }

                if (annotationDeleted)
                    break;
            }

            if (!annotationDeleted)
            {
                Console.WriteLine($"Annotation '{annotationName}' not found.");
            }
            else
            {
                // Save the modified PDF.
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Annotation '{annotationName}' deleted. Saved to '{outputPath}'.");
            }
        }
    }
}
