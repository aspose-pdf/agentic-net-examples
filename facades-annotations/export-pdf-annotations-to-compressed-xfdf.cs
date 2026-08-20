using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing annotations
        const string inputPdfPath = "input.pdf";

        // Output file will contain the compressed XFDF data
        const string outputCompressedXfdfPath = "annotations.xfdf.gz";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor facade and bind it to the loaded document
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(pdfDoc);

                // Export all annotations to an in‑memory stream in XFDF format
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(xfdfStream);

                    // Prepare the output file stream and wrap it with GZip for compression
                    using (FileStream outFile = new FileStream(outputCompressedXfdfPath,
                                                              FileMode.Create,
                                                              FileAccess.Write))
                    using (GZipStream gzip = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        // Reset the XFDF stream position before copying
                        xfdfStream.Position = 0;
                        xfdfStream.CopyTo(gzip);
                    }
                }
            }
        }

        Console.WriteLine($"Annotations exported and compressed to '{outputCompressedXfdfPath}'.");
    }
}