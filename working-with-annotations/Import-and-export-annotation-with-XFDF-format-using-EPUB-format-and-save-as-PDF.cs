using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XFDF related types (optional but safe)

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";          // source EPUB file
        const string pdfPath  = "output.pdf";          // target PDF file
        const string xfdfPath = "annotations.xfdf";    // intermediate XFDF file

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB file into a PDF Document using EpubLoadOptions
        using (Document doc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) At this point you could modify the document,
            // remove annotations, etc. For this example we simply re‑import.

            // Import the annotations back from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the final document as a PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EPUB converted to PDF with annotations.");
        Console.WriteLine($"PDF saved to: {pdfPath}");
        Console.WriteLine($"XFDF saved to: {xfdfPath}");
    }
}