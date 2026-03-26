using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";
        const string notesTxt = "speaker_notes.txt"; // optional: store extracted notes

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // -------------------------------------------------------------------
            // 1. Extract annotation contents from each PDF page – they will become
            //    speaker notes for the corresponding slide (saved to a text file).
            // -------------------------------------------------------------------
            var pageNotes = new Dictionary<int, string>();
            foreach (Page page in pdfDoc.Pages)
            {
                var sb = new StringBuilder();
                foreach (Annotation annot in page.Annotations)
                {
                    // Most annotation types expose the textual content via the "Contents" property.
                    // For TextAnnotation and FreeTextAnnotation we also have a dedicated property.
                    if (annot is TextAnnotation txtAnnot)
                    {
                        if (!string.IsNullOrEmpty(txtAnnot.Contents))
                            sb.AppendLine(txtAnnot.Contents);
                    }
                    else if (annot is FreeTextAnnotation ftAnnot)
                    {
                        if (!string.IsNullOrEmpty(ftAnnot.Contents))
                            sb.AppendLine(ftAnnot.Contents);
                    }
                    else if (!string.IsNullOrEmpty(annot.Contents))
                    {
                        sb.AppendLine(annot.Contents);
                    }
                }
                var notes = sb.ToString().Trim();
                if (!string.IsNullOrEmpty(notes))
                    pageNotes[page.Number] = notes;
            }

            // -------------------------------------------------------------------
            // 2. Convert PDF to PPTX using Aspose.Pdf only (no Aspose.Slides required).
            // -------------------------------------------------------------------
            pdfDoc.Save(outputPptx, SaveFormat.Pptx);

            // -------------------------------------------------------------------
            // 3. (Optional) Persist the extracted notes to a separate text file so the
            //    consumer can manually add them to the PPTX if required.
            // -------------------------------------------------------------------
            if (pageNotes.Count > 0)
            {
                var notesBuilder = new StringBuilder();
                foreach (var kvp in pageNotes)
                {
                    notesBuilder.AppendLine($"--- Page {kvp.Key} ---");
                    notesBuilder.AppendLine(kvp.Value);
                    notesBuilder.AppendLine();
                }
                File.WriteAllText(notesTxt, notesBuilder.ToString());
                Console.WriteLine($"Speaker notes extracted to: {notesTxt}");
            }
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}
