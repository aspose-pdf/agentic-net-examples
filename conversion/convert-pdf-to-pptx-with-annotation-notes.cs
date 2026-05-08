using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfToPptxWithSpeakerNotes
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Step 1: Load PDF and extract annotation contents per page
        var pageNotes = new Dictionary<int, string>(); // key = page number (1‑based)

        using (Document pdfDoc = new Document(pdfPath))
        {
            // Extract notes from each page
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                var notes = new List<string>();

                // Annotations collection is 1‑based as well
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];
                    string text = null;

                    // Prefer the Contents property; if empty, try Title (available on markup annotations only)
                    if (!string.IsNullOrEmpty(ann.Contents))
                    {
                        text = ann.Contents;
                    }
                    else if (ann is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                    {
                        text = markup.Title;
                    }

                    if (!string.IsNullOrEmpty(text))
                        notes.Add(text.Trim());
                }

                if (notes.Count > 0)
                    pageNotes[pageNum] = string.Join(Environment.NewLine, notes);
            }

            // Step 2: Convert PDF to PPTX using Aspose.Pdf's native conversion
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        // NOTE: Adding speaker notes to the generated PPTX requires Aspose.Slides, which is a separate product.
        // Since the project does not reference Aspose.Slides, the notes are not injected into the PPTX here.
        // If Aspose.Slides becomes available, the extracted `pageNotes` dictionary can be used to populate
        // each slide's speaker notes as demonstrated in the original example.

        Console.WriteLine($"Conversion completed. PPTX saved to '{pptxPath}'.");
    }
}
