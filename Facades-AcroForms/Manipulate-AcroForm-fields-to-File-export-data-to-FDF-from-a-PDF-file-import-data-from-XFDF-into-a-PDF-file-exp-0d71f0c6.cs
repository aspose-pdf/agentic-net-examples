using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AcroFormDataExample
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath          = "input_form.pdf";
        const string exportedFdfPath  = "fields_export.fdf";
        const string exportedXfdfPath = "fields_export.xfdf";
        const string xfdfImportPath   = "data_to_import.xfdf";
        const string xmlSourcePath    = "data_source.xml";
        const string xmlToFdfPath     = "xml_converted.fdf";
        const string outputPdfPath    = "output_form.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF form using the Form facade (implements IDisposable)
        // -----------------------------------------------------------------
        using (Form form = new Form(pdfPath))
        {
            // -------------------------------------------------------------
            // 2. Export form field data to FDF
            // -------------------------------------------------------------
            using (FileStream fdfStream = new FileStream(exportedFdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }
            Console.WriteLine($"Form fields exported to FDF: {exportedFdfPath}");

            // -------------------------------------------------------------
            // 3. Export form field data to XFDF
            // -------------------------------------------------------------
            using (FileStream xfdfStream = new FileStream(exportedXfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }
            Console.WriteLine($"Form fields exported to XFDF: {exportedXfdfPath}");

            // -------------------------------------------------------------
            // 4. Import field data from an existing XFDF file into the PDF
            // -------------------------------------------------------------
            if (File.Exists(xfdfImportPath))
            {
                using (FileStream importXfdf = new FileStream(xfdfImportPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXfdf(importXfdf);
                }
                Console.WriteLine($"XFDF data imported from: {xfdfImportPath}");
            }
            else
            {
                Console.WriteLine($"XFDF import file not found (skipped): {xfdfImportPath}");
            }

            // -------------------------------------------------------------
            // 5. Convert an XML form data file to FDF using FormDataConverter
            // -------------------------------------------------------------
            if (File.Exists(xmlSourcePath))
            {
                using (FileStream xmlStream = new FileStream(xmlSourcePath, FileMode.Open, FileAccess.Read))
                using (FileStream fdfDest   = new FileStream(xmlToFdfPath, FileMode.Create, FileAccess.Write))
                {
                    FormDataConverter.ConvertXmlToFdf(xmlStream, fdfDest);
                }
                Console.WriteLine($"XML converted to FDF: {xmlToFdfPath}");
            }
            else
            {
                Console.WriteLine($"XML source file not found (skipped): {xmlSourcePath}");
            }

            // -------------------------------------------------------------
            // 6. Save the modified PDF (the Form facade inherits SaveableFacade)
            // -------------------------------------------------------------
            form.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to: {outputPdfPath}");
        }
    }
}