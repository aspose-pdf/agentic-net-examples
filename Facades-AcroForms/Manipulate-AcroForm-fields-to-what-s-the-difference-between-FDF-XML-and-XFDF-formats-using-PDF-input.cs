using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AcroFormExportImportDemo
{
    static void Main()
    {
        // Input PDF that contains AcroForm fields
        const string inputPdf = "sample_form.pdf";

        // Output files for the three export formats
        const string fdfFile  = "form_data.fdf";
        const string xmlFile  = "form_data.xml";
        const string xfdfFile = "form_data.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // --------------------------------------------------------------------
        // Export form data to the three supported formats using Aspose.Pdf.Facades.Form
        // --------------------------------------------------------------------
        // 1. Bind the Form facade to the source PDF
        using (Form formExporter = new Form())
        {
            formExporter.BindPdf(inputPdf);

            // Export to FDF (Adobe's original Forms Data Format – binary)
            using (FileStream fdfStream = new FileStream(fdfFile, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportFdf(fdfStream);
            }

            // Export to XML (simple XML representation of field names/values)
            using (FileStream xmlStream = new FileStream(xmlFile, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXml(xmlStream);
            }

            // Export to XFDF (XML‑based FDF – retains FDF structure but in XML)
            using (FileStream xfdfStream = new FileStream(xfdfFile, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine("Export completed:");
        Console.WriteLine($"  FDF  -> {fdfFile}");
        Console.WriteLine($"  XML  -> {xmlFile}");
        Console.WriteLine($"  XFDF -> {xfdfFile}");

        // --------------------------------------------------------------------
        // Demonstrate import of one of the exported formats back into a PDF.
        // Here we import the XML representation into a copy of the original PDF.
        // --------------------------------------------------------------------
        const string importedPdf = "imported_form.pdf";

        // Create a copy of the original PDF to avoid overwriting it
        File.Copy(inputPdf, importedPdf, true);

        using (Form formImporter = new Form())
        {
            // Bind to the copy that will receive the imported data
            formImporter.BindPdf(importedPdf);

            // Import the previously exported XML data
            using (FileStream xmlStream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
            {
                formImporter.ImportXml(xmlStream);
            }

            // Save the PDF with the imported field values
            formImporter.Save(importedPdf);
        }

        Console.WriteLine($"Import completed: XML data applied to {importedPdf}");
        Console.WriteLine("Demo finished. Review the exported files to see the differences:");
        Console.WriteLine("- FDF  : Binary format used by legacy Adobe tools.");
        Console.WriteLine("- XML  : Plain XML listing field names and values.");
        Console.WriteLine("- XFDF : XML version of FDF, preserving FDF structure while being readable.");
    }
}