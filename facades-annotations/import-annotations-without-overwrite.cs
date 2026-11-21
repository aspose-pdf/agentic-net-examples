using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source_with_new_annotations.pdf";
        const string destinationPdfPath = "existing_annotations.pdf";
        const string outputPdfPath = "merged_annotations.pdf";
        const string tempXfdfPath = "temp_annotations.xfdf";

        // Verify that the required PDFs exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }

        // Copy the destination PDF to a new file so the original remains unchanged
        File.Copy(destinationPdfPath, outputPdfPath, true);

        // ------------------------------------------------------------
        // Step 1: Export annotations from the source PDF to an XFDF file
        // ------------------------------------------------------------
        using (var srcDoc = new Document(sourcePdfPath))
        {
            using (var xfdfStream = new FileStream(tempXfdfPath, FileMode.Create, FileAccess.Write))
            {
                srcDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        // ------------------------------------------------------------
        // Step 2: Import the exported XFDF into the copied destination PDF
        // Existing annotations are preserved – the import operation adds
        // new annotations without overwriting the ones already present.
        // ------------------------------------------------------------
        using (var outDoc = new Document(outputPdfPath))
        {
            using (var xfdfStream = new FileStream(tempXfdfPath, FileMode.Open, FileAccess.Read))
            {
                outDoc.ImportAnnotationsFromXfdf(xfdfStream);
            }
            outDoc.Save(outputPdfPath);
        }

        // Clean up the temporary XFDF file
        if (File.Exists(tempXfdfPath))
        {
            File.Delete(tempXfdfPath);
        }

        Console.WriteLine($"Annotations merged successfully. Output saved to '{outputPdfPath}'.");
    }
}
