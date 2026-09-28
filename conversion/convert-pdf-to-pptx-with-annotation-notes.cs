using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // STEP 1: Load PDF and collect annotation contents per page (optional)
        var pageNotes = new Dictionary<int, string>(); // 1‑based page number

        using (Document pdfDoc = new Document(pdfPath))
        {
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                var notes = new List<string>();

                foreach (Annotation annotation in page.Annotations)
                {
                    if (!string.IsNullOrEmpty(annotation.Contents))
                        notes.Add(annotation.Contents.Trim());
                }

                if (notes.Count > 0)
                    pageNotes[i] = string.Join(Environment.NewLine, notes);
            }

            // STEP 2: Convert PDF to PPTX directly with Aspose.Pdf
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // NOTE: Adding speaker notes to the generated PPTX would require Aspose.Slides,
        // which is not referenced in this project. The extracted annotation text is
        // retained in the `pageNotes` dictionary for further processing if needed.

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'.");
    }
}
