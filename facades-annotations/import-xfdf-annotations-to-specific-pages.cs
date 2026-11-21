using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, XFDF file with annotations, and output PDF paths
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Define the page range where the XFDF annotations should be applied (e.g., pages 2 to 4)
        const string pageRange = "2-4";

        // Validate input files
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

        // Load the target PDF document
        Document targetDoc = new Document(pdfPath);

        // Load XFDF annotations into a temporary PDF document
        Document xfdfDoc = new Document();
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        {
            XfdfReader.ReadAnnotations(xfdfStream, xfdfDoc);
        }

        // Parse the page range (expected format "start-end")
        int startPage = 1;
        int endPage = targetDoc.Pages.Count;
        if (!string.IsNullOrWhiteSpace(pageRange))
        {
            string[] parts = pageRange.Split('-');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int s) &&
                int.TryParse(parts[1], out int e))
            {
                startPage = s;
                endPage = e;
            }
        }

        // Ensure the range is within the document bounds
        startPage = Math.Max(startPage, 1);
        endPage = Math.Min(endPage, Math.Min(targetDoc.Pages.Count, xfdfDoc.Pages.Count));

        // Copy annotations from the XFDF document to the target document for the specified pages
        for (int i = startPage; i <= endPage; i++)
        {
            Page srcPage = xfdfDoc.Pages[i];
            Page dstPage = targetDoc.Pages[i];
            foreach (Annotation ann in srcPage.Annotations)
            {
                // Clone the annotation to avoid reference issues
                dstPage.Annotations.Add((Annotation)ann.Clone());
            }
        }

        // Save the modified PDF with the imported annotations
        targetDoc.Save(outputPath);

        Console.WriteLine($"Annotations imported to pages {pageRange} and saved as '{outputPath}'.");
    }
}
