using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // MemoryStream to hold the exported XFDF data
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            // Bind the PDF and export annotations from pages 1 and 2
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);
                // Export all annotation types (null) for the specified page range
                editor.ExportAnnotationsXfdf(xfdfStream, 1, 2, (AnnotationType[])null);
            }

            // Reset the stream position for any further processing
            xfdfStream.Position = 0;

            // Example: read the XFDF content as a string (optional)
            using (StreamReader reader = new StreamReader(xfdfStream))
            {
                string xfdfContent = reader.ReadToEnd();
                Console.WriteLine("Exported XFDF:");
                Console.WriteLine(xfdfContent);
            }
        }
    }
}