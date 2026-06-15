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

        // 1. Load PDF and extract annotation contents per page
        var pageNotes = new Dictionary<int, List<string>>(); // page index (1‑based) -> list of annotation texts

        using (Document pdfDoc = new Document(pdfPath))
        {
            for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
            {
                Page page = pdfDoc.Pages[pageIdx];
                var notes = new List<string>();

                // Annotations collection is 1‑based as well
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Most annotation types expose the Contents property (text entered by the user)
                    if (!string.IsNullOrWhiteSpace(ann.Contents))
                        notes.Add(ann.Contents.Trim());

                    // For markup annotations (sticky notes) the Title can also be useful
                    if (ann is MarkupAnnotation markup && !string.IsNullOrWhiteSpace(markup.Title))
                        notes.Add($"Title: {markup.Title.Trim()}");
                }

                if (notes.Count > 0)
                    pageNotes[pageIdx] = notes;
            }

            // 2. Convert PDF to PPTX using Aspose.Pdf's built‑in converter
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // NOTE: Adding speaker notes to the generated PPTX would require Aspose.Slides.
        // The current project does not reference Aspose.Slides, so we limit the solution to
        // PDF‑to‑PPTX conversion. The extracted annotation texts are retained in the
        // `pageNotes` dictionary and can be used later if Aspose.Slides is added.

        Console.WriteLine($"Conversion completed. PPTX saved to '{pptxPath}'.");
    }
}
