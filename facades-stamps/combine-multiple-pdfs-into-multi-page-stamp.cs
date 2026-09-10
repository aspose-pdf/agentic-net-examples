using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create sample stamp PDFs (stamp1.pdf, stamp2.pdf, stamp3.pdf).
        // ---------------------------------------------------------------------
        string[] stampSources = { "stamp1.pdf", "stamp2.pdf", "stamp3.pdf" };
        for (int i = 0; i < stampSources.Length; i++)
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment($"Stamp page {i + 1}"));
                doc.Save(stampSources[i]);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Create the target PDF that will receive the stamp.
        // ---------------------------------------------------------------------
        string targetPdfPath = "target.pdf";
        using (Document targetDoc = new Document())
        {
            Page page = targetDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Target PDF page 1"));
            targetDoc.Save(targetPdfPath);
        }

        // ---------------------------------------------------------------------
        // 3. Combine the stamp PDFs into a single multi‑page PDF.
        // ---------------------------------------------------------------------
        string combinedStampPath = "combinedStamp.pdf";
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(stampSources, combinedStampPath);

        // ---------------------------------------------------------------------
        // 4. Prepare the PdfFileStamp facade for the target document.
        // ---------------------------------------------------------------------
        string outputPdfPath = "targetStamped.pdf";
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(targetPdfPath);

        // ---------------------------------------------------------------------
        // 5. Create a Stamp object bound to the first page of the combined PDF.
        // ---------------------------------------------------------------------
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(combinedStampPath, 1); // first page of combined stamp PDF
        stamp.IsBackground = true;           // place behind existing content
        stamp.Pages = null;                  // apply to all pages of the target PDF

        // ---------------------------------------------------------------------
        // 6. Apply the stamp and save the result.
        // ---------------------------------------------------------------------
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdfPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}