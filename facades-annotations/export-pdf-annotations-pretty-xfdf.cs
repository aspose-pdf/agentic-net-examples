using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF into the annotation editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Export all annotations to XFDF (pretty‑printed)
            using (MemoryStream ms = new MemoryStream())
            {
                // Use the overload that accepts a string[] and cast null to that type
                int startPage = 1;
                int endPage = editor.Document.Pages.Count;
                editor.ExportAnnotationsXfdf(ms, startPage, endPage, (string[])null);
                ms.Position = 0; // rewind for reading

                // Load the XFDF XML and save it with indentation (pretty‑print)
                XDocument xfdfDoc = XDocument.Load(ms);
                xfdfDoc.Save(outputXfdf);
            }
        }

        Console.WriteLine($"Annotations exported to '{outputXfdf}'.");
    }
}
