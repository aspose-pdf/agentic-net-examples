using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string notesTmpPath = "slide_notes.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load PDF and extract annotation contents (speaker notes)
        // -----------------------------------------------------------------
        var pageNotes = new Dictionary<int, List<string>>(); // page index (1‑based) -> notes

        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                var notesForPage = new List<string>();

                // Annotations collection also uses 1‑based indexing
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Title is only available on markup annotations; cast safely.
                    string title = (ann as MarkupAnnotation)?.Title;
                    string text = string.Empty;

                    if (!string.IsNullOrWhiteSpace(title))
                        text = title;

                    if (!string.IsNullOrWhiteSpace(ann.Contents))
                    {
                        if (!string.IsNullOrWhiteSpace(text))
                            text += ": ";
                        text += ann.Contents;
                    }

                    if (!string.IsNullOrWhiteSpace(text))
                        notesForPage.Add(text);
                }

                if (notesForPage.Count > 0)
                    pageNotes[pageNum] = notesForPage;
            }

            // -----------------------------------------------------------------
            // 2. Convert PDF to PPTX using Aspose.Pdf's native SaveFormat
            // -----------------------------------------------------------------
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // -----------------------------------------------------------------
        // 3. (Optional) Persist extracted notes to a text file for reference.
        // -----------------------------------------------------------------
        if (pageNotes.Count > 0)
        {
            using (var writer = new StreamWriter(notesTmpPath, false))
            {
                foreach (var kvp in pageNotes)
                {
                    writer.WriteLine($"Page {kvp.Key} notes:");
                    foreach (var note in kvp.Value)
                    {
                        writer.WriteLine($"- {note}");
                    }
                    writer.WriteLine();
                }
            }
        }

        Console.WriteLine($"PDF converted to PPTX: {pptxPath}");
        if (File.Exists(notesTmpPath))
            Console.WriteLine($"Extracted notes saved to: {notesTmpPath}");
    }
}
