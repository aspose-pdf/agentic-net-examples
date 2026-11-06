using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing annotations
        const string xfdfPath = "annotations.xfdf";       // intermediate XFDF file
        const string gzOutputPath = "annotations.xfdf.gz";// final compressed file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Export all annotations from the PDF to XFDF using PdfAnnotationEditor (Facades API)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);

                int startPage = 1;
                int endPage = editor.Document.Pages.Count; // export from first to last page

                // Explicitly cast null to string[] to select the overload that accepts annotation type names.
                // Passing null (without cast) is ambiguous because there is also an overload that accepts AnnotationType[].
                using (FileStream xfdfStream = File.Create(xfdfPath))
                {
                    editor.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, (string[])null);
                }
            }

            // Compress the generated XFDF file with GZip for efficient storage
            using (FileStream sourceStream = File.OpenRead(xfdfPath))
            using (FileStream destinationStream = File.Create(gzOutputPath))
            using (GZipStream gzipStream = new GZipStream(destinationStream, CompressionMode.Compress))
            {
                sourceStream.CopyTo(gzipStream);
            }

            // Optionally delete the intermediate uncompressed XFDF file
            File.Delete(xfdfPath);

            Console.WriteLine($"Annotations exported and compressed to: {gzOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
