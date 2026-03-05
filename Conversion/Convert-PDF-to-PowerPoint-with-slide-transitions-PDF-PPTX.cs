using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF file
        const string outputPpt = "output.pptx"; // destination PowerPoint file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // Add slide transition effects to each PDF page.
            // PdfPageEditor allows setting TransitionType and TransitionDuration.
            // These settings are carried over when the PDF is converted to PPTX.
            // -----------------------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(pdfDoc);

                // Process all pages (1‑based indexing)
                int pageCount = pdfDoc.Pages.Count;
                int[] pages = new int[pageCount];
                for (int i = 0; i < pageCount; i++) pages[i] = i + 1;
                pageEditor.ProcessPages = pages;

                // Example transition: vertical blinds (constant defined in PdfPageEditor)
                pageEditor.TransitionType = PdfPageEditor.BLINDV;

                // Duration of the transition in seconds
                pageEditor.TransitionDuration = 2;

                // Apply the changes to the document
                pageEditor.ApplyChanges();
            }

            // -----------------------------------------------------------------
            // Convert the PDF (now with transitions) to PPTX.
            // Use PptxSaveOptions – a SaveOptions subclass – as required for
            // non‑PDF output formats.
            // -----------------------------------------------------------------
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the document as a PowerPoint presentation
            pdfDoc.Save(outputPpt, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with slide transitions: {outputPpt}");
    }
}