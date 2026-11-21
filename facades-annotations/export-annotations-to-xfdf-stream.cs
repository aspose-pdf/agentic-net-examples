using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // needed for AnnotationType if you ever use it

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF document to the annotation editor.
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            annotationEditor.BindPdf(inputPath);

            // Export annotations from pages 1 and 2 into an XFDF memory stream.
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // The overload that takes a string[] is ambiguous with the one that takes AnnotationType[].
                // Cast null to string[] (or pass an empty string array) to select the desired overload.
                annotationEditor.ExportAnnotationsXfdf(xfdfStream, 1, 2, (string[])null);

                // Reset stream position for reading.
                xfdfStream.Position = 0;

                // Example: read the XFDF content as a string.
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XFDF:");
                    Console.WriteLine(xfdfContent);
                }
            }
        }
    }
}
