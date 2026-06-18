using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // original PDF
        const string xfdfPath  = "backup.xfdf";    // XFDF backup containing annotations
        const string outputPath = "output.pdf";    // PDF after restoring annotations

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Delete all annotations on every page
            foreach (Page page in doc.Pages)
            {
                // AnnotationCollection.Delete() removes all annotations in the collection
                page.Annotations.Delete();
            }

            // Re‑import annotations from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations restored and saved to '{outputPath}'.");
    }
}