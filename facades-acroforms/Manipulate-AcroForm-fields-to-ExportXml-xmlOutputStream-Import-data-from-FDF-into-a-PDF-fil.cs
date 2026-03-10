using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input_form.pdf";   // PDF with AcroForm fields
        const string xmlOutputPath = "form_data.xml";   // Destination for exported XML
        const string fdfInputPath = "data.fdf";         // Source FDF file to import
        const string outputPdfPath = "output_form.pdf"; // PDF after importing FDF data

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfInputPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfInputPath}");
            return;
        }

        // -------------------------------------------------
        // Export AcroForm field data to XML (ExportXml)
        // -------------------------------------------------
        using (var formExporter = new Aspose.Pdf.Facades.Form())
        {
            // Bind the PDF that contains the form
            formExporter.BindPdf(inputPdfPath);

            // Export the form fields to an XML stream
            using (FileStream xmlStream = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXml(xmlStream);
            }

            // No need to call Save() here; ExportXml does not modify the PDF
        }

        // -------------------------------------------------
        // Import data from an FDF file into a PDF (ImportFdf)
        // -------------------------------------------------
        using (var formImporter = new Aspose.Pdf.Facades.Form())
        {
            // Bind the same (or another) PDF to receive the FDF data
            formImporter.BindPdf(inputPdfPath);

            // Read the FDF stream and import its field values
            using (FileStream fdfStream = new FileStream(fdfInputPath, FileMode.Open, FileAccess.Read))
            {
                formImporter.ImportFdf(fdfStream);
            }

            // Save the updated PDF to a new file
            formImporter.Save(outputPdfPath);
        }

        Console.WriteLine("Form data exported to XML and FDF data imported successfully.");
    }
}