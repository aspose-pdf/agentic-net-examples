using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class ExportAnnotationsExample
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Ensure the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Optional: set the Aspose.Pdf native API folder if the default location is not used.
        // This prevents the runtime from trying to start a non‑existent process.
        // Uncomment and adjust the path to the folder that contains the AsposePdfApi_* binaries.
        // Aspose.Pdf.Facades.PdfAnnotationEditor.ApiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "net10.0");

        // Create a memory stream that will hold the XFDF data
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            // Initialize the PdfAnnotationEditor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfPath);

                // Define the annotation types to export.
                AnnotationType[] annotTypes = new AnnotationType[]
                {
                    AnnotationType.Text,
                    AnnotationType.Highlight,
                    AnnotationType.Square,
                    AnnotationType.Circle,
                    AnnotationType.Line,
                    AnnotationType.Stamp,
                    AnnotationType.FreeText,
                    AnnotationType.Ink,
                    AnnotationType.Popup,
                    AnnotationType.Link,
                    AnnotationType.FileAttachment,
                    AnnotationType.Sound,
                    AnnotationType.Movie,
                    AnnotationType.Screen,
                    AnnotationType.Widget,
                    AnnotationType.Watermark,
                    // AnnotationType.ThreeD,   // Not available in current Aspose.PDF version
                    AnnotationType.RichMedia,
                    // AnnotationType.Redact,   // Not available in current Aspose.PDF version
                    AnnotationType.Caret,
                    AnnotationType.StrikeOut,
                    AnnotationType.Underline,
                    AnnotationType.Squiggly
                };

                // Export annotations from pages 1 to 2 into the memory stream
                editor.ExportAnnotationsXfdf(xfdfStream, 1, 2, annotTypes);
            }

            // Reset the stream position to the beginning for further processing
            xfdfStream.Position = 0;

            // Example: read the XFDF content as a UTF‑8 string (optional)
            using (StreamReader reader = new StreamReader(xfdfStream, Encoding.UTF8))
            {
                string xfdfContent = reader.ReadToEnd();
                Console.WriteLine("Exported XFDF content:");
                Console.WriteLine(xfdfContent);
            }

            // At this point, xfdfStream contains the XFDF data and can be passed to other APIs.
        }
    }
}
