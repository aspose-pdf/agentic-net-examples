using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes (Document, MdLoadOptions, etc.)
using Aspose.Pdf.Facades;            // Facade classes for metadata manipulation

class Program
{
    static void Main()
    {
        // Paths to the source Markdown file and the target PDF file
        const string mdPath = "input.md";
        const string pdfPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(mdPath))
        {
            Console.Error.WriteLine($"Source file not found: {mdPath}");
            return;
        }

        // Load the Markdown file and convert it to a PDF document
        // MdLoadOptions is used to control the conversion; default constructor is sufficient here
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        using (Document pdfDoc = new Document(mdPath, mdLoadOptions))
        {
            // -----------------------------------------------------------------
            // Modify document properties (metadata) using PdfFileInfo facade
            // -----------------------------------------------------------------
            // PdfFileInfo provides getters/setters for standard PDF metadata fields.
            // It works on an existing Document instance via its constructor.
            using (PdfFileInfo info = new PdfFileInfo(pdfDoc))
            {
                // Set standard metadata properties
                info.Title = "Converted Markdown Document";
                info.Author = "John Doe";
                info.Subject = "Demonstration of MD to PDF conversion with metadata";
                info.Keywords = "Aspose.Pdf, Markdown, PDF, Metadata";

                // Optionally set custom metadata entries
                info.SetMetaInfo("CustomProperty1", "CustomValue1");
                info.SetMetaInfo("CustomProperty2", "CustomValue2");

                // Save the updated metadata back into the PDF document.
                // SaveNewInfo writes only the changed metadata without altering the rest of the PDF.
                info.SaveNewInfo(pdfPath);
            }

            // If you prefer to overwrite the whole document (including content) after
            // modifying metadata, you can also call pdfDoc.Save(pdfPath) here.
            // In this example we used SaveNewInfo to avoid re‑writing page content.
        }

        Console.WriteLine($"PDF created and metadata updated: {pdfPath}");
    }
}