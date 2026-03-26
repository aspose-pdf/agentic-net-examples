using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";
        const int startPage = 2; // inclusive, 1‑based
        const int endPage = 4;   // inclusive, 1‑based

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the target PDF document.
        using (Document targetDoc = new Document(inputPdfPath))
        {
            // Load XFDF annotations into a temporary PDF document.
            Document xfdfDoc = new Document();
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                XfdfReader.ReadAnnotations(xfdfStream, xfdfDoc);
            }

            // Ensure the target document has enough pages for the requested range.
            while (targetDoc.Pages.Count < endPage)
            {
                targetDoc.Pages.Add();
            }

            // Import annotations from the specified page range of the XFDF document.
            for (int pageIndex = startPage; pageIndex <= endPage && pageIndex <= xfdfDoc.Pages.Count; pageIndex++)
            {
                Page sourcePage = xfdfDoc.Pages[pageIndex];
                Page destinationPage = targetDoc.Pages[pageIndex];

                foreach (Annotation sourceAnnotation in sourcePage.Annotations)
                {
                    // Clone the annotation to avoid reference issues.
                    Annotation clonedAnnotation = (Annotation)sourceAnnotation.Clone();
                    destinationPage.Annotations.Add(clonedAnnotation);
                }
            }

            // Save the updated PDF.
            targetDoc.Save(outputPdfPath);
            Console.WriteLine($"Annotations imported to pages {startPage}-{endPage} and saved as '{outputPdfPath}'.");
        }
    }
}