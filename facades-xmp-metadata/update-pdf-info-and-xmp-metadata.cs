using System;
using Aspose.Pdf;               // <-- added for XmpValue
using Aspose.Pdf.Facades;   // Facade classes: PdfFileInfo, PdfXmpMetadata, XmpValue

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF file info facade (lifecycle managed by using)
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            // ----- Modify standard document properties -----
            fileInfo.Title  = "Updated Document Title";
            fileInfo.Author = "Jane Doe";

            // ----- Modify XMP metadata -----
            // Bind the XMP facade to the same underlying document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Bind to the document instance used by PdfFileInfo
                xmp.BindPdf(fileInfo.Document);

                // Add or replace XMP entries (example: Dublin Core creator)
                xmp.Add("dc:creator", new XmpValue("Jane Doe"));
                // Add a custom namespace entry (example)
                xmp.Add("myNamespace:customProperty", new XmpValue("CustomValue"));
            }

            // ----- Save the PDF with the updated info and XMP metadata -----
            bool saved = fileInfo.SaveNewInfoWithXmp(outputPdf);
            Console.WriteLine(saved
                ? $"PDF saved successfully to '{outputPdf}'."
                : "Failed to save the PDF with updated XMP metadata.");
        }
    }
}
