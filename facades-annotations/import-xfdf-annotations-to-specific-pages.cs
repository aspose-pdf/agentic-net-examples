using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string xfdfPath       = "annotations.xfdf";
        const string outputPdfPath  = "output.pdf";
        const int startPage = 2; // first page of the range (1‑based)
        const int endPage   = 4; // last page of the range (inclusive)

        // Verify that source files exist
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

        // Load the target PDF document
        Document targetDoc = new Document(inputPdfPath);

        // Load XFDF annotations into a temporary PDF document
        Document xfdfDoc = new Document();
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        {
            // The XfdfReader reads XFDF and creates annotation objects on the pages of xfdfDoc
            XfdfReader.ReadAnnotations(xfdfStream, xfdfDoc);
        }

        // Ensure the requested page range is within the bounds of both documents
        int maxPage = Math.Min(Math.Min(targetDoc.Pages.Count, xfdfDoc.Pages.Count), endPage);
        for (int pageNumber = startPage; pageNumber <= maxPage; pageNumber++)
        {
            Page srcPage = xfdfDoc.Pages[pageNumber];
            Page dstPage = targetDoc.Pages[pageNumber];

            // Copy each annotation from the XFDF page to the target PDF page
            foreach (Annotation ann in srcPage.Annotations)
            {
                // Clone creates a deep copy so the annotation can be added to another document
                dstPage.Annotations.Add((Annotation)ann.Clone());
            }
        }

        // Save the resulting PDF with the imported annotations
        targetDoc.Save(outputPdfPath);
        Console.WriteLine($"Annotations imported to pages {startPage}-{endPage} and saved to '{outputPdfPath}'.");
    }
}
