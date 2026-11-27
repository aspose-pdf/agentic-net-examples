using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(pdfPath);

        // Import FDF annotations using the Form facade (methods accept streams, not file paths).
        using (Form form = new Form(doc))
        using (FileStream fdfStream = File.OpenRead(fdfPath))
        {
            form.ImportFdf(fdfStream);
        }

        // Delete annotation with index 1 on page 3 (pages are 1‑based, annotation collection is 0‑based).
        if (doc.Pages.Count >= 3)
        {
            Page page = doc.Pages[3];
            if (page.Annotations.Count > 1)
            {
                page.Annotations.Delete(1); // removes the second annotation (index 1)
            }
        }

        // Save the modified PDF using PdfAnnotationEditor (binds to the in‑memory Document).
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
