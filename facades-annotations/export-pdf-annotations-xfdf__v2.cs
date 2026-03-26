using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class AnnotationExporter
{
    // Exports all annotations from a PDF (provided as a byte array) to XFDF and returns the XFDF as a byte array.
    public static byte[] ExportAnnotationsToXfdf(byte[] pdfBytes)
    {
        if (pdfBytes == null)
            throw new ArgumentNullException(nameof(pdfBytes));

        using (var pdfStream = new MemoryStream(pdfBytes))
        {
            var pdfDocument = new Document(pdfStream);
            using (var xfdfStream = new MemoryStream())
            {
                pdfDocument.ExportAnnotationsToXfdf(xfdfStream);
                // ToArray reads from the beginning of the stream, no need to reset Position.
                return xfdfStream.ToArray();
            }
        }
    }

    // Example usage that works without external files.
    public static void Main()
    {
        // 1. Create a simple PDF in memory.
        var doc = new Document();
        doc.Pages.Add();
        var page = doc.Pages[1];

        // 2. Add a sample text annotation (optional, demonstrates that something is exported).
        var textAnnot = new TextAnnotation(page, new Rectangle(100, 700, 200, 750))
        {
            Title = "Sample",
            Contents = "This is a test annotation."
        };
        page.Annotations.Add(textAnnot);

        // 3. Save the PDF to a byte array.
        byte[] pdfBytes;
        using (var ms = new MemoryStream())
        {
            doc.Save(ms);
            pdfBytes = ms.ToArray();
        }

        // 4. Export annotations to XFDF.
        byte[] xfdfBytes = ExportAnnotationsToXfdf(pdfBytes);

        // 5. (Optional) Write the XFDF to a file for verification.
        File.WriteAllBytes("annotations.xfdf", xfdfBytes);
        Console.WriteLine($"Exported {xfdfBytes.Length} bytes of XFDF data.");
    }
}