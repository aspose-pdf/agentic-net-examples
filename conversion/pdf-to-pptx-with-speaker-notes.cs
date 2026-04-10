using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Rectangle when needed explicitly

class PdfToPptxWithSpeakerNotes
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF
        const string pptxPath = "output.pptx"; // destination PPTX

        // -----------------------------------------------------------------
        // 1. Ensure a source PDF exists – create a minimal one with an annotation
        //    if the file is missing. This prevents the FileNotFoundException at
        //    runtime and also gives us something to extract notes from.
        // -----------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
            Console.WriteLine($"Sample PDF created at '{pdfPath}'.");
        }

        // -----------------------------------------------------------------
        // 2. Load PDF, extract annotation contents per page (to be used as notes)
        // -----------------------------------------------------------------
        List<string> pageNotes = new List<string>();

        using (Document pdfDoc = new Document(pdfPath))
        {
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                StringBuilder sb = new StringBuilder();

                // Collect all annotation contents on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    if (!string.IsNullOrEmpty(annotation.Contents))
                    {
                        sb.AppendLine(annotation.Contents);
                    }
                }

                pageNotes.Add(sb.ToString().Trim());
            }

            // -----------------------------------------------------------------
            // 3. Convert PDF to PPTX using Aspose.Pdf's native support
            // -----------------------------------------------------------------
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // -----------------------------------------------------------------
        // 4. (Optional) Add speaker notes – requires Aspose.Slides assembly.
        // -----------------------------------------------------------------
        // The following block is kept as a reference. Uncomment and add a reference
        // to Aspose.Slides if you need to embed the extracted notes into the PPTX.
        /*
        using (Presentation presentation = new Presentation(pptxPath))
        {
            for (int slideIndex = 0; slideIndex < presentation.Slides.Count; slideIndex++)
            {
                string noteText = slideIndex < pageNotes.Count ? pageNotes[slideIndex] : string.Empty;

                if (!string.IsNullOrEmpty(noteText))
                {
                    var notesSlide = presentation.Slides[slideIndex].NotesSlideManager?.NotesSlide
                                   ?? presentation.Slides[slideIndex].NotesSlideManager?.AddNotesSlide();
                    notesSlide.NotesTextFrame.Text = noteText;
                }
            }
            presentation.Save(pptxPath, SaveFormat.Pptx);
        }
        */

        Console.WriteLine($"PDF converted to PPTX saved at '{pptxPath}'.");
    }

    // ---------------------------------------------------------------------
    // Helper: creates a very simple PDF with a single page and a text
    // annotation. The annotation's Contents property will be used as a speaker
    // note later on.
    // ---------------------------------------------------------------------
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add a simple text annotation (sticky note) with some content
            // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid any ambiguity.
            Aspose.Pdf.Rectangle noteRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
            TextAnnotation note = new TextAnnotation(page, noteRect)
            {
                Title = "Sample Note",
                Contents = "This is a sample speaker note extracted from the PDF annotation."
            };
            page.Annotations.Add(note);

            // Save the PDF to the supplied path
            doc.Save(path);
        }
    }
}
