using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputGz = "annotations.xfdf.gz";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor facade and bind the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Export all annotations to an in‑memory XFDF stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset stream position before reading

                // Compress the XFDF data with GZip and write to the output file
                using (FileStream fileOut = new FileStream(outputGz, FileMode.Create, FileAccess.Write))
                using (GZipStream gzip = new GZipStream(fileOut, CompressionLevel.Optimal))
                {
                    xfdfStream.CopyTo(gzip);
                }
            }

            // Release resources held by the editor (optional but recommended)
            editor.Close();
        }

        Console.WriteLine($"Annotations exported and compressed to '{outputGz}'.");
    }
}