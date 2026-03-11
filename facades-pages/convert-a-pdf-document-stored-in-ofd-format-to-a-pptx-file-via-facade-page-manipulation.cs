using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputOfdPath  = "input.ofd";      // OFD source file
        const string outputPptxPath = "output.pptx";   // Desired PPTX output

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"File not found: {inputOfdPath}");
            return;
        }

        // Load OFD, convert to PDF in memory, then save as PPTX using facade page editor
        using (Document pdfDoc = new Document())
        {
            // Load OFD using the appropriate load options (input‑only format)
            pdfDoc.LoadFrom(inputOfdPath, new OfdLoadOptions());

            // OPTIONAL: manipulate pages via PdfPageEditor facade (example: no changes)
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(pdfDoc);
                // Example manipulation – rotate first page 0 degrees (no effect)
                // pageEditor.Rotation = 0;
                // pageEditor.ApplyChanges(); // Apply any changes made
                pageEditor.ApplyChanges();
            }

            // Save the resulting document as PPTX using explicit save options
            pdfDoc.Save(outputPptxPath, new PptxSaveOptions());
        }

        Console.WriteLine($"Conversion completed: '{outputPptxPath}'");
    }
}