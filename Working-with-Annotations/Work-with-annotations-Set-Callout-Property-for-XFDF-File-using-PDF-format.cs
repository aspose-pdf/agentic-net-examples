using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outPdf    = "output.pdf";
        const string outXfdf   = "updated_annotations.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load PDF and import existing XFDF annotations
        using (Document doc = new Document(pdfPath))
        {
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Iterate all pages and their annotations
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // Target only FreeTextAnnotation objects
                    if (annot is FreeTextAnnotation freeText)
                    {
                        // Define a simple callout line (two points)
                        // Adjust coordinates as needed for your document
                        Aspose.Pdf.Point[] callout = new Aspose.Pdf.Point[]
                        {
                            new Aspose.Pdf.Point(100, 500), // start point (anchor)
                            new Aspose.Pdf.Point(150, 550)  // end point (tip)
                        };

                        freeText.Callout = callout;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outPdf);

            // Export the updated annotations back to XFDF (optional)
            doc.ExportAnnotationsToXfdf(outXfdf);
        }

        Console.WriteLine($"PDF saved to '{outPdf}'.");
        Console.WriteLine($"Updated XFDF saved to '{outXfdf}'.");
    }
}