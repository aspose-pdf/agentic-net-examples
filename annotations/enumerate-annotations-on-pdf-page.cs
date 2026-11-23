using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Annotation base class lives here

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indices are 1‑based; page 14 is accessed via Pages[14]
            if (doc.Pages.Count < 14)
            {
                Console.Error.WriteLine("The document has fewer than 14 pages.");
                return;
            }

            Page page = doc.Pages[14];

            // Aspose.Pdf does not provide a Pdf3DAnnotation class – 3D annotations are not supported.
            // Therefore we cannot set a DefaultViewIndex. The code below simply demonstrates how to
            // enumerate existing annotations on the page (if any) using the common Annotation base class.
            foreach (Annotation annot in page.Annotations)
            {
                Console.WriteLine($"Found annotation of type {annot.AnnotationType}");
            }

            // Save the (unchanged) PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
