using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
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

        // -------------------------------------------------------------------
        // 1. Load the PDF and extract annotation text per page (optional).
        //    The extracted notes are kept in an array but are not written to the
        //    PPTX because Aspose.Slides – the library required for manipulating
        //    notes slides – is not referenced in this project.
        // -------------------------------------------------------------------
        string[] pageNotes;
        using (Document pdfDoc = new Document(pdfPath))
        {
            int pageCount = pdfDoc.Pages.Count;
            pageNotes = new string[pageCount];

            for (int i = 1; i <= pageCount; i++)
            {
                Page page = pdfDoc.Pages[i];
                StringBuilder sb = new StringBuilder();

                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is TextAnnotation textAnnot)
                    {
                        if (!string.IsNullOrEmpty(textAnnot.Title))
                            sb.AppendLine($"Title: {textAnnot.Title}");

                        if (!string.IsNullOrEmpty(textAnnot.Contents))
                            sb.AppendLine(textAnnot.Contents);
                    }
                }

                pageNotes[i - 1] = sb.ToString().Trim();
            }

            // -------------------------------------------------------------------
            // 2. Convert the PDF directly to PPTX using Aspose.Pdf.
            //    Aspose.Pdf provides a built‑in SaveFormat.Pptx, eliminating the
            //    need for the separate Aspose.Slides assembly.
            // -------------------------------------------------------------------
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF converted to PPTX: {pptxPath}");
    }
}
