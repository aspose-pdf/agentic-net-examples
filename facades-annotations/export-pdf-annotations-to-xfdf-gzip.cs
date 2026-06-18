using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportAnnotationsCompressed
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string outputGzipPath = "annotations.xfdf.gz";   // compressed XFDF output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor with the loaded document
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(pdfDoc))
            {
                // Export all annotations to an in‑memory stream (XFDF format)
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(xfdfStream);

                    // Prepare the stream for reading
                    xfdfStream.Position = 0;

                    // Compress the XFDF data using GZip and write to the output file
                    using (FileStream fileOut = new FileStream(outputGzipPath, FileMode.Create, FileAccess.Write))
                    using (GZipStream gzipOut = new GZipStream(fileOut, CompressionMode.Compress))
                    {
                        xfdfStream.CopyTo(gzipOut);
                    }
                }
            }
        }

        Console.WriteLine($"Annotations exported and compressed to '{outputGzipPath}'.");
    }
}