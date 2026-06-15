using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Source PDF with form fields
        const string xmlPath = "form_fields.xml";    // Destination XML file
        const string importedPdfPath = "imported.pdf"; // PDF after re‑import

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // Export form fields to XML (archival)
        // -------------------------------------------------
        using (Form form = new Form(pdfPath))               // Load PDF into Form facade
        {
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);                  // Write form data to XML stream
            }
        }

        Console.WriteLine($"Form fields exported to XML: {xmlPath}");

        // -------------------------------------------------
        // Re‑import the XML back into a PDF (optional demo)
        // -------------------------------------------------
        // Create a copy of the original PDF to avoid overwriting it
        File.Copy(pdfPath, importedPdfPath, true);

        using (Form form = new Form(importedPdfPath))       // Load the copy for import
        {
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);                  // Load form data from XML
            }

            form.Save();                                    // Persist changes to the same file
        }

        Console.WriteLine($"XML data imported back into PDF: {importedPdfPath}");
    }
}