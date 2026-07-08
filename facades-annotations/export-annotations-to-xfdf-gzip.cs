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
        const string outputGzPath = "annotations.xfdf.gz"; // compressed XFDF output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                // Bind the loaded document to the editor (required before export)
                annotEditor.BindPdf(pdfDoc);

                // Export all annotations to an in‑memory XFDF stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(xfdfStream);
                    xfdfStream.Position = 0; // reset for reading

                    // Compress the XFDF data with GZip and write to the output file
                    using (FileStream outFile = new FileStream(outputGzPath, FileMode.Create, FileAccess.Write))
                    using (GZipStream gzip = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        xfdfStream.CopyTo(gzip);
                    }
                }
            }
        }

        Console.WriteLine($"Annotations exported and compressed to '{outputGzPath}'.");
    }
}