using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF with a form
        const string outputXfdfPath = "interaction_log.xfdf"; // XFDF is XML‑based

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (core API only)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // After the form has been submitted (or any user interaction),
            // export all annotations (including form field data) to XFDF.
            // XFDF is an XML representation of the form data and annotations.
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
            }

            Console.WriteLine($"Interaction log exported to XML (XFDF) at '{outputXfdfPath}'.");
        }
    }
}