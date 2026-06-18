using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // AnnotationType enum

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // MemoryStream will hold the exported XFDF data
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            // Initialize the annotation editor and bind the PDF document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPdf);

            // Export annotations from pages 1 to 2 for all annotation types
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));
            editor.ExportAnnotationsXfdf(xfdfStream, 1, 2, allTypes);

            // Reset the stream position so it can be read or passed elsewhere
            xfdfStream.Position = 0;

            // Example: read the XFDF content as a string (optional)
            using (StreamReader reader = new StreamReader(xfdfStream))
            {
                string xfdfContent = reader.ReadToEnd();
                Console.WriteLine("Exported XFDF:");
                Console.WriteLine(xfdfContent);
            }

            // xfdfStream now contains the XFDF data and can be used for further processing
        }
    }
}