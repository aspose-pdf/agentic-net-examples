using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_caret.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // XFDF definition of a caret annotation on page 0 (zero‑based index).
        // rect = left,bottom,right,top (in user space units).
        string xfdf = @"<?xml version='1.0' encoding='UTF-8'?>
<xfdf xmlns='http://ns.adobe.com/xfdf/' xml:space='preserve'>
  <annots>
    <caret page='0' rect='100,500,120,520' name='Caret1' title='John Doe' subject='Caret Annotation' contents='Sample caret' />
  </annots>
</xfdf>";

        // Load the PDF, import the XFDF annotation, and save.
        using (Document doc = new Document(inputPdf))
        {
            // Convert the XFDF string to a stream for the reader.
            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xfdf)))
            {
                // Import annotations from the XFDF stream into the document.
                XfdfReader.ReadAnnotations(ms, doc);
            }

            // Persist the changes.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Caret annotation added and saved to '{outputPdf}'.");
    }
}
