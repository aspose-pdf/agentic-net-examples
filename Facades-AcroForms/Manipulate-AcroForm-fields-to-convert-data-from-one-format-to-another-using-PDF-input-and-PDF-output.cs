using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (form template), source XML data,
        // the PDF that will contain the populated form, and the final FDF file.
        const string inputPdfPath   = "template.pdf";
        const string xmlDataPath    = "data.xml";
        const string outputPdfPath  = "filled.pdf";
        const string outputFdfPath  = "data.fdf";

        // Verify that the required files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the PDF form and import XML data into its fields.
        // ------------------------------------------------------------
        // The Form constructor that takes two file names sets the source
        // PDF and the destination PDF where changes will be saved.
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(inputPdfPath, outputPdfPath))
        {
            // Open the XML file as a stream and import it.
            using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Persist the populated form to the output PDF.
            form.Save();
        }

        // ------------------------------------------------------------
        // 2. Export the filled form data to FDF format.
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form exportForm = new Aspose.Pdf.Facades.Form(outputPdfPath))
        {
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                exportForm.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine("Form data conversion completed successfully.");
    }
}