using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the intermediate XFDF file and the final PDF
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Export existing annotations from the source PDF to an XFDF file
        // -----------------------------------------------------------------
        using (Document sourceDoc = new Document(inputPdfPath))
        using (FileStream xfdfWriteStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            // ExportAnnotationsToXfdf writes the annotation data to the provided stream.
            sourceDoc.ExportAnnotationsToXfdf(xfdfWriteStream);
        }

        // ---------------------------------------------------------------
        // 2. Load the source PDF again and import the previously exported
        //    XFDF data back into the document
        // ---------------------------------------------------------------
        using (Document importDoc = new Document(inputPdfPath))
        using (FileStream xfdfReadStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
        {
            // ImportAnnotationsFromXfdf reads the XFDF stream and adds the annotations.
            importDoc.ImportAnnotationsFromXfdf(xfdfReadStream);

            // Save the document that now contains the re‑imported annotations.
            importDoc.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // 3. Verify round‑trip integrity by comparing annotation counts
        // ---------------------------------------------------------------
        using (Document originalDoc = new Document(inputPdfPath))
        using (Document roundTripDoc = new Document(outputPdfPath))
        {
            int originalAnnotationCount = 0;
            foreach (Page page in originalDoc.Pages)
                originalAnnotationCount += page.Annotations.Count;

            int roundTripAnnotationCount = 0;
            foreach (Page page in roundTripDoc.Pages)
                roundTripAnnotationCount += page.Annotations.Count;

            Console.WriteLine($"Original annotation count: {originalAnnotationCount}");
            Console.WriteLine($"After round‑trip annotation count: {roundTripAnnotationCount}");
        }
    }
}
