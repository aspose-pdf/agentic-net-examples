using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string templatePath = "template.pdf";               // PDF containing the source annotations
        string[] targetPaths = { "target1.pdf", "target2.pdf" };   // PDFs that will receive the annotations

        // Verify template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Export annotations from the template to an in‑memory XFDF byte array
        byte[] xfdfBytes;
        using (var xfdfStream = new MemoryStream())
        using (var templateDoc = new Document(templatePath))
        using (var exporter = new PdfAnnotationEditor())
        {
            exporter.BindPdf(templateDoc);
            exporter.ExportAnnotationsToXfdf(xfdfStream);
            // Capture the XFDF data for reuse
            xfdfBytes = xfdfStream.ToArray();
        }

        // Process each target PDF
        foreach (string targetPath in targetPaths)
        {
            if (!File.Exists(targetPath))
            {
                Console.Error.WriteLine($"Target file not found: {targetPath}");
                continue;
            }

            // Load target document and import the previously exported annotations
            using (var targetDoc = new Document(targetPath))
            using (var importer = new PdfAnnotationEditor())
            using (var importStream = new MemoryStream(xfdfBytes))
            {
                importer.BindPdf(targetDoc);
                importer.ImportAnnotationsFromXfdf(importStream);

                // Save the modified document (overwrites the original)
                importer.Save(targetPath);
            }

            Console.WriteLine($"Annotations copied to: {targetPath}");
        }
    }
}
