using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF with form fields
        const string exportXmlPath  = "form_data.xml"; // archival XML file
        const string importedPdfPath = "imported.pdf"; // PDF after re‑importing data

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Export form fields to XML (archival)
        // ------------------------------------------------------------
        // Form(string) constructor loads the PDF.
        using (Form formExporter = new Form(inputPdfPath))
        {
            // Create a writable stream for the XML output.
            using (FileStream xmlOut = new FileStream(exportXmlPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field values (except button values) to the XML stream.
                formExporter.ExportXml(xmlOut);
            }
        }

        // ------------------------------------------------------------
        // 2. Re‑import the previously exported XML into a new PDF
        // ------------------------------------------------------------
        // Form(string, string) constructor loads the source PDF and sets the output file name.
        using (Form formImporter = new Form(inputPdfPath, importedPdfPath))
        {
            // Open the XML file for reading.
            using (FileStream xmlIn = new FileStream(exportXmlPath, FileMode.Open, FileAccess.Read))
            {
                // Import the field values from the XML stream into the PDF.
                formImporter.ImportXml(xmlIn);
            }

            // Save the PDF with the imported data. Save() writes to the file specified in the constructor.
            formImporter.Save();
        }

        Console.WriteLine($"Form fields exported to XML: {exportXmlPath}");
        Console.WriteLine($"Form fields re‑imported and saved as PDF: {importedPdfPath}");
    }
}