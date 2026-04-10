using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportAnnotationsCompressed
{
    static void Main()
    {
        // Input PDF file containing annotations
        const string inputPdfPath = "input.pdf";
        // Output compressed XFDF file (GZip format)
        const string outputGzPath = "annotations.xfdf.gz";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor and bind it to the loaded document
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(pdfDoc);

                // Export all annotations to an in‑memory XFDF stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(xfdfStream);
                    // Reset stream position before reading
                    xfdfStream.Position = 0;

                    // Compress the XFDF data using GZip and write to the output file
                    using (FileStream fileOut = new FileStream(outputGzPath, FileMode.Create, FileAccess.Write))
                    using (GZipStream gzipOut = new GZipStream(fileOut, CompressionLevel.Optimal))
                    {
                        xfdfStream.CopyTo(gzipOut);
                    }
                }
            }
        }

        Console.WriteLine($"Annotations exported and compressed to '{outputGzPath}'.");
    }
}