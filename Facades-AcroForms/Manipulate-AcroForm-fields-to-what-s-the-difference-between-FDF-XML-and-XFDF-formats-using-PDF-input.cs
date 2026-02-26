using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string fdfPath           = "form_data.fdf";
        const string xmlPath           = "form_data.xml";
        const string xfdfPath          = "form_data.xfdf";
        const string fdfFromXmlPath    = "converted_from_xml.fdf";
        const string xmlFromFdfPath    = "converted_from_fdf.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Export AcroForm data from the PDF into the three formats
            // ------------------------------------------------------------
            using (Aspose.Pdf.Facades.Form exportForm = new Aspose.Pdf.Facades.Form(inputPdfPath))
            {
                // Export to FDF
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
                {
                    exportForm.ExportFdf(fdfStream);
                }

                // Export to XML (Aspose's proprietary XML representation)
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    exportForm.ExportXml(xmlStream);
                }

                // Export to XFDF (XML‑based FDF, widely supported by PDF viewers)
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    exportForm.ExportXfdf(xfdfStream);
                }
            }

            Console.WriteLine("Exported form data to FDF, XML and XFDF.");

            // ------------------------------------------------------------
            // 2. Convert between XML and FDF using FormDataConverter
            // ------------------------------------------------------------
            // XML → FDF
            using (FileStream srcXml = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream destFdf = new FileStream(fdfFromXmlPath, FileMode.Create, FileAccess.Write))
            {
                Aspose.Pdf.Facades.FormDataConverter.ConvertXmlToFdf(srcXml, destFdf);
            }

            // FDF → XML
            using (FileStream srcFdf = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream destXml = new FileStream(xmlFromFdfPath, FileMode.Create, FileAccess.Write))
            {
                Aspose.Pdf.Facades.FormDataConverter.ConvertFdfToXml(srcFdf, destXml);
            }

            Console.WriteLine("Performed XML↔FDF conversions using FormDataConverter.");

            // ------------------------------------------------------------
            // 3. Import each format back into a fresh copy of the PDF
            // ------------------------------------------------------------
            // Helper method to import and save
            void ImportAndSave(string sourcePdf, string targetPdf, Action<Aspose.Pdf.Facades.Form> importAction)
            {
                using (Aspose.Pdf.Facades.Form importForm = new Aspose.Pdf.Facades.Form(sourcePdf, targetPdf))
                {
                    importAction(importForm);
                    importForm.Save(); // Saves to targetPdf
                }
            }

            // Import from FDF
            ImportAndSave(
                inputPdfPath,
                "pdf_filled_from_fdf.pdf",
                form =>
                {
                    using (FileStream fdfIn = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                    {
                        form.ImportFdf(fdfIn);
                    }
                });

            // Import from XML
            ImportAndSave(
                inputPdfPath,
                "pdf_filled_from_xml.pdf",
                form =>
                {
                    using (FileStream xmlIn = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                    {
                        form.ImportXml(xmlIn);
                    }
                });

            // Import from XFDF
            ImportAndSave(
                inputPdfPath,
                "pdf_filled_from_xfdf.pdf",
                form =>
                {
                    using (FileStream xfdfIn = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
                    {
                        form.ImportXfdf(xfdfIn);
                    }
                });

            Console.WriteLine("Imported data back into new PDFs (FDF, XML, XFDF).");

            // ------------------------------------------------------------
            // 4. Summary of format differences (output to console)
            // ------------------------------------------------------------
            Console.WriteLine("\n--- Format Summary ---");
            Console.WriteLine("FDF  : Binary representation of form data. Small size, not human‑readable.");
            Console.WriteLine("XML  : Aspose‑specific XML describing fields and values. Useful for programmatic processing.");
            Console.WriteLine("XFDF : XML‑based FDF (standardized). Human‑readable and widely supported by PDF viewers.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}