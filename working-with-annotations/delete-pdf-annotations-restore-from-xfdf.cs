using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string xfdfBackupPath = "backup.xfdf";
        const string outputPdfPath  = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfBackupPath))
        {
            Console.Error.WriteLine($"XFDF backup not found: {xfdfBackupPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Delete all annotations on each page
            foreach (Page page in doc.Pages)
            {
                // AnnotationCollection.Delete() removes every annotation on the page
                page.Annotations.Delete();
            }

            // Re‑import annotations from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfBackupPath);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations restored and saved to '{outputPdfPath}'.");
    }
}