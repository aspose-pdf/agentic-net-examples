using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the FDF file containing annotations.
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output_with_annotations.pdf";

        // Verify that both files exist before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Open the FDF file as a stream and import its annotations into the PDF.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // The static FdfReader reads all annotations from the stream and
                // attaches them to the appropriate pages inside the document.
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // OPTIONAL: Verify that annotations have been placed on the expected pages.
            // This loop prints each annotation's type together with the page number
            // it resides on. The page number is taken from the Page object (1‑based).
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    Console.WriteLine(
                        $"Annotation of type {annotation.AnnotationType} " +
                        $"found on page {page.Number}");
                }
            }

            // Save the modified PDF with the imported annotations.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and PDF saved to '{outputPath}'.");
    }
}