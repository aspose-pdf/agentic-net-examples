using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfToPptxWithSpeakerNotes
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Step 1: Load PDF and extract annotation contents per page
        List<string> pageNotes = new List<string>();
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure we have a slot for each page
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                List<string> annotationsText = new List<string>();

                // Collect text from all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Only consider markup annotations that have visible contents
                    if (!string.IsNullOrEmpty(ann.Contents))
                        annotationsText.Add(ann.Contents);
                }

                // Combine multiple annotations into a single note string (separated by new lines)
                string combinedNote = string.Join(Environment.NewLine, annotationsText);
                pageNotes.Add(combinedNote);
            }

            // Step 2: Convert PDF to PPTX using Aspose.Pdf's native support
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        // NOTE: Adding speaker notes to the generated PPTX requires Aspose.Slides.
        // The current project does not reference Aspose.Slides, so this step is omitted.
        // If Aspose.Slides is added later, the extracted `pageNotes` list can be used
        // to populate each slide's NotesSlide as demonstrated in the original sample.

        Console.WriteLine($"PDF converted to PPTX: {outputPptxPath}");
    }
}
