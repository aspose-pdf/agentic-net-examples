using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace contains Document, EpubLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Input EPUB file, temporary XFDF file for annotations, and final PDF output.
        const string epubPath = "input.epub";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify the EPUB source exists.
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB document as a PDF using EpubLoadOptions.
        // The using block ensures deterministic disposal of the Document object.
        using (Document doc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Export any existing annotations to an XFDF file.
            // This creates a portable representation of the annotations.
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

            // At this point you could modify the XFDF file externally if needed.
            // For demonstration we simply import the same XFDF back into the document.

            // Import annotations from the XFDF file into the PDF document.
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from '{xfdfPath}'.");

            // Save the resulting PDF. No SaveOptions are required because the
            // default format is PDF.
            doc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        }
    }
}