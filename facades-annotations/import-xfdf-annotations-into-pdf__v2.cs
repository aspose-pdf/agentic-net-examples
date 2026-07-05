using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Ensure the source files exist – prevents FileNotFoundException at runtime.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Open the PDF with read/write access because the editor will modify the stream.
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
        // Open the XFDF as read‑only.
        using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        // Memory stream that will hold the resulting PDF – no temporary file on disk.
        using (MemoryStream outputStream = new MemoryStream())
        // Annotation editor facade.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF stream to the editor.
            editor.BindPdf(pdfStream);

            // Import all annotations from the XFDF stream into the bound PDF.
            editor.ImportAnnotationsFromXfdf(xfdfStream);

            // Save the modified PDF into the in‑memory stream.
            editor.Save(outputStream);

            // Persist the in‑memory PDF to the final file location (single write, no temp files).
            File.WriteAllBytes(outputPath, outputStream.ToArray());
        }

        Console.WriteLine($"Annotations imported successfully. Output saved to {outputPath}");
    }
}
