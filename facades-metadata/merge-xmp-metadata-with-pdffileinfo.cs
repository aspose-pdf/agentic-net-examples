using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_merged.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a minimal
        // PDF so that PdfFileInfo can be instantiated without throwing.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            var doc = new Document();
            doc.Pages.Add(); // add a blank page
            doc.Save(inputPath);
        }

        // Load existing PDF file information (author, title, etc.).
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);

        // Example: add a custom metadata entry.
        fileInfo.SetMetaInfo("CustomKey", "CustomValue");

        // Load the PDF document to work with XMP metadata via Document.Metadata.
        Document pdfDoc = new Document(inputPath);

        // ------------------------------------------------------------
        // Register the namespaces that will be used in the XMP packet.
        // ------------------------------------------------------------
        pdfDoc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");
        pdfDoc.Metadata.RegisterNamespaceUri("pdf", "http://ns.adobe.com/pdf/1.3/");

        // ------------------------------------------------------------
        // Merge standard file‑info properties into the XMP packet.
        // ------------------------------------------------------------
        // Author → dc:creator
        if (!string.IsNullOrEmpty(fileInfo.Author))
            pdfDoc.Metadata["dc:creator"] = fileInfo.Author;

        // Title → dc:title
        if (!string.IsNullOrEmpty(fileInfo.Title))
            pdfDoc.Metadata["dc:title"] = fileInfo.Title;

        // Subject → dc:description
        if (!string.IsNullOrEmpty(fileInfo.Subject))
            pdfDoc.Metadata["dc:description"] = fileInfo.Subject;

        // Keywords → pdf:Keywords (custom namespace)
        if (!string.IsNullOrEmpty(fileInfo.Keywords))
            pdfDoc.Metadata["pdf:Keywords"] = fileInfo.Keywords;

        // Persist the merged metadata (both PdfFileInfo entries and updated XMP) to a new file.
        // Save the document – XMP metadata is automatically written.
        pdfDoc.Save(outputPath);
        Console.WriteLine($"Metadata merged and saved to '{outputPath}'.");
    }
}
