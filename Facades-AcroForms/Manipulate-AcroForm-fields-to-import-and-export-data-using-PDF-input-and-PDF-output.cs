using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // PDF with AcroForm fields
        const string outputPdf  = "output.pdf";     // PDF after import
        const string exportFdf  = "exported.fdf";   // Exported FDF data
        const string importFdf  = "data.fdf";       // FDF data to import
        const string exportXml  = "exported.xml";   // Exported XML data
        const string importXml  = "data.xml";       // XML data to import
        const string exportXfdf = "exported.xfdf";  // Exported XFDF data
        const string importXfdf = "data.xfdf";      // XFDF data to import

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Export form data to various formats (FDF, XML, XFDF)
        // -----------------------------------------------------------------
        using (Form formExporter = new Form())
        {
            // Bind the source PDF
            formExporter.BindPdf(inputPdf);

            // Export to FDF
            using (FileStream fdfStream = new FileStream(exportFdf, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportFdf(fdfStream);
            }

            // Export to XML
            using (FileStream xmlStream = new FileStream(exportXml, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXml(xmlStream);
            }

            // Export to XFDF
            using (FileStream xfdfStream = new FileStream(exportXfdf, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXfdf(xfdfStream);
            }
        }

        // -----------------------------------------------------------------
        // Import form data from various formats and save the updated PDF
        // -----------------------------------------------------------------
        using (Form formImporter = new Form())
        {
            // Bind the same source PDF (could be a different one)
            formImporter.BindPdf(inputPdf);

            // Import from FDF if the file exists
            if (File.Exists(importFdf))
            {
                using (FileStream fdfIn = new FileStream(importFdf, FileMode.Open, FileAccess.Read))
                {
                    formImporter.ImportFdf(fdfIn);
                }
            }

            // Import from XML if the file exists
            if (File.Exists(importXml))
            {
                using (FileStream xmlIn = new FileStream(importXml, FileMode.Open, FileAccess.Read))
                {
                    formImporter.ImportXml(xmlIn);
                }
            }

            // Import from XFDF if the file exists
            if (File.Exists(importXfdf))
            {
                using (FileStream xfdfIn = new FileStream(importXfdf, FileMode.Open, FileAccess.Read))
                {
                    formImporter.ImportXfdf(xfdfIn);
                }
            }

            // Save the PDF with imported data
            formImporter.Save(outputPdf);
        }

        Console.WriteLine("Form data export and import completed successfully.");
    }
}