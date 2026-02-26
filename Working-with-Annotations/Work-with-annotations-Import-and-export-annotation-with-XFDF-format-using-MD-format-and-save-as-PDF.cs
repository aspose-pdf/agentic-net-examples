using System;
using System.IO;
using Aspose.Pdf;

class AnnotationXfdfExample
{
    static void Main()
    {
        // Paths for the source PDF, the XFDF file and the final PDF
        const string sourcePdfPath = "source.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the PDF that already contains annotations
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // --------------------------------------------------------
            // 2. Export all annotations to an XFDF file
            // --------------------------------------------------------
            sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");
        }

        // ------------------------------------------------------------
        // 3. Create a new PDF (could be a copy of the original or a blank doc)
        //    Here we load the same source PDF to demonstrate import.
        // ------------------------------------------------------------
        using (Document targetDoc = new Document(sourcePdfPath))
        {
            // --------------------------------------------------------
            // 4. Import the previously exported annotations from XFDF
            // --------------------------------------------------------
            targetDoc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from XFDF: {xfdfPath}");

            // --------------------------------------------------------
            // 5. Save the resulting PDF with the imported annotations
            // --------------------------------------------------------
            targetDoc.Save(outputPdfPath);
            Console.WriteLine($"Resulting PDF saved: {outputPdfPath}");
        }
    }
}