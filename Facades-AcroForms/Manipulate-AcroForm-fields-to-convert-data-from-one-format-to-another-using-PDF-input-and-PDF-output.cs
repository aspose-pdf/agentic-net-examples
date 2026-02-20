using System;
using System.IO;
using Aspose.Pdf.Facades;

class AcroFormDataConverter
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xmlDataPath = "formData.xml";
        const string fdfDataPath = "formData.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the PDF and export its AcroForm data to XML format
            // ------------------------------------------------------------
            using (Form pdfForm = new Form())
            {
                pdfForm.BindPdf(inputPdfPath);

                // Export form fields to XML
                using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Create, FileAccess.Write))
                {
                    pdfForm.ExportXml(xmlStream);
                }
            }

            // ------------------------------------------------------------
            // 2. Convert the exported XML to FDF using FormDataConverter
            // ------------------------------------------------------------
            FormDataConverter converter = new FormDataConverter();
            using (FileStream xmlRead = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
            using (FileStream fdfWrite = new FileStream(fdfDataPath, FileMode.Create, FileAccess.Write))
            {
                // Static conversion method: XML -> FDF
                FormDataConverter.ConvertXmlToFdf(xmlRead, fdfWrite);
            }

            // ------------------------------------------------------------
            // 3. Load the original PDF again and import the FDF data back
            // ------------------------------------------------------------
            using (Form pdfForm = new Form())
            {
                pdfForm.BindPdf(inputPdfPath);

                // Import the previously generated FDF data
                using (FileStream fdfRead = new FileStream(fdfDataPath, FileMode.Open, FileAccess.Read))
                {
                    pdfForm.ImportFdf(fdfRead);
                }

                // Save the modified PDF to the output file
                pdfForm.Save(outputPdfPath);
            }

            Console.WriteLine("AcroForm data conversion completed successfully.");
            Console.WriteLine($"Exported XML: {xmlDataPath}");
            Console.WriteLine($"Converted FDF: {fdfDataPath}");
            Console.WriteLine($"Result PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}