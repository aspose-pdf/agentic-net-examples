using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Annotations;        // Required for Annotation and MarkupAnnotation types

class PdfToPptxWithNotes
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string notesPath = "output_notes.txt"; // optional: store extracted notes separately

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // 1. Load PDF and extract annotation contents per page
        List<string> pageNotes = new List<string>();
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Ensure we have a slot for each page
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                pageNotes.Add(string.Empty);

            // Collect annotation contents
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                List<string> notesForPage = new List<string>();

                foreach (Annotation ann in page.Annotations)
                {
                    // Title exists only on markup annotations; fall back to Contents otherwise
                    string noteText = string.Empty;
                    if (ann is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                    {
                        noteText = markup.Title;
                    }
                    else if (!string.IsNullOrEmpty(ann.Contents))
                    {
                        noteText = ann.Contents;
                    }

                    if (!string.IsNullOrEmpty(noteText))
                        notesForPage.Add(noteText);
                }

                if (notesForPage.Count > 0)
                    pageNotes[i - 1] = string.Join(Environment.NewLine, notesForPage);
            }

            // 2. Convert PDF to PPTX using Aspose.Pdf's native support
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // 3. (Optional) Persist extracted notes to a plain‑text file so the user can manually add them to the PPTX.
        //    Direct manipulation of PPTX speaker notes requires Aspose.Slides, which is a separate product.
        //    If Aspose.Slides is available, the notes can be added programmatically; otherwise we provide them here.
        try
        {
            using (StreamWriter writer = new StreamWriter(notesPath, false))
            {
                for (int i = 0; i < pageNotes.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(pageNotes[i]))
                    {
                        writer.WriteLine($"--- Slide {i + 1} Notes ---");
                        writer.WriteLine(pageNotes[i]);
                        writer.WriteLine();
                    }
                }
            }
            Console.WriteLine($"Extracted notes saved to '{notesPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write notes file: {ex.Message}");
        }

        Console.WriteLine($"Conversion completed. PPTX saved to '{pptxPath}'.");
    }
}
