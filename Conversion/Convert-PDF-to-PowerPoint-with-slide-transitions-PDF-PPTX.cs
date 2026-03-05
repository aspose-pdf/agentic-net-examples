using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdf))
        {
            // Apply slide transition effects to each page using PdfPageEditor
            using (Aspose.Pdf.Facades.PdfPageEditor pageEditor = new Aspose.Pdf.Facades.PdfPageEditor())
            {
                pageEditor.BindPdf(pdfDoc);

                // Example transition: Blind Horizontal (BLINDH) with a 2‑second duration
                pageEditor.TransitionType = Aspose.Pdf.Facades.PdfPageEditor.BLINDH;
                pageEditor.TransitionDuration = 2;

                // Commit the changes to the document
                pageEditor.ApplyChanges();
            }

            // Configure PPTX save options (optional settings can be adjusted here)
            Aspose.Pdf.PptxSaveOptions pptxOptions = new Aspose.Pdf.PptxSaveOptions();
            // pptxOptions.SlidesAsImages = false; // default behavior renders editable shapes

            // Convert and save the PDF as a PowerPoint presentation
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputPptx}'");
    }
}