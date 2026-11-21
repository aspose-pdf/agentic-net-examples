using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using Document (PdfFileEditor is not needed for annotation changes)
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
        {
            // Example: modify the first annotation on the first page
            // Page and Annotation collections are 1‑based in Aspose.Pdf
            Aspose.Pdf.Page page = pdfDoc.Pages[1];

            if (page.Annotations.Count > 0)
            {
                // Retrieve the annotation (index 1 = first annotation)
                Aspose.Pdf.Annotations.Annotation annotation = page.Annotations[1];

                // Apply the read‑only flag
                annotation.Flags = Aspose.Pdf.Annotations.AnnotationFlags.ReadOnly;
            }
            else
            {
                Console.WriteLine("No annotations found on the first page.");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only annotation flag: {outputPath}");
    }
}