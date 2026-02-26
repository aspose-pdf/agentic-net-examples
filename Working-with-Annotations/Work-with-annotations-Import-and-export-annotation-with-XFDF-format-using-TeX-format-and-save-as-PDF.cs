using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XFDF related methods (optional)

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF with annotations
        const string xfdfPath = "annotations.xfdf";   // temporary XFDF file
        const string outputPdf = "output.pdf";        // PDF after re‑importing annotations
        const string texPath = "output.tex";          // TeX representation of the PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Export annotations to XFDF ----------
        using (Document doc = new Document(inputPdf))
        {
            // Writes all existing annotations to the XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ---------- Import annotations from XFDF and save ----------
        using (Document doc = new Document(inputPdf))
        {
            // Reads the XFDF file and adds its annotations to the document
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the PDF with the imported annotations
            doc.Save(outputPdf);

            // Additionally, export the same document to TeX format
            TeXSaveOptions texOptions = new TeXSaveOptions();
            doc.Save(texPath, texOptions);
        }

        Console.WriteLine("Annotations exported to XFDF, re‑imported, PDF saved, and TeX file generated.");
    }
}