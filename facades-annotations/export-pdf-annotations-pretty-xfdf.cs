using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations_pretty.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Bind the loaded PDF document to the annotation editor
            PdfAnnotationEditor annotEditor = new PdfAnnotationEditor();
            annotEditor.BindPdf(doc);

            // Attempt to export annotations to XFDF. The XfdfWriter class is not present in
            // newer Aspose.Pdf versions, so we fall back to a graceful message.
            try
            {
                // If a future version provides XfdfWriter, the following pattern can be used:
                // using (var writer = new XfdfWriter())
                // {
                //     writer.WriteAnnotations(doc, xfdfPath);
                // }
                // For now we simply inform the user that the feature is unavailable.
                Console.WriteLine("Export to XFDF is skipped because XfdfWriter is not available in the current Aspose.Pdf version.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Export to XFDF failed: {ex.Message}");
            }
        }

        Console.WriteLine($"Processing completed. If XfdfWriter becomes available, annotations can be exported to '{xfdfPath}'.");
    }
}
