using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportAnnotationsCompressed
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF with annotations
        const string outputGzipPath = "annotations.xfdf.gz"; // compressed XFDF output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Export annotations to XFDF using PdfAnnotationEditor (Facades API)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPdfPath);

            // Export all annotations into an in‑memory stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // rewind for reading

                // Compress the XFDF data with GZip and write to the output file
                using (FileStream fileStream = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
                {
                    xfdfStream.CopyTo(gzipStream);
                }
            }
        }

        Console.WriteLine($"Annotations exported and compressed to '{outputGzipPath}'.");
    }
}