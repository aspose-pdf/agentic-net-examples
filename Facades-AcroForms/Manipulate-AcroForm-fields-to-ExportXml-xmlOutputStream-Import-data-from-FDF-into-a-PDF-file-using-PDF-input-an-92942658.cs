using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the AcroForm
        const string inputPdfPath = "input.pdf";
        // FDF file that holds field values to be imported
        const string fdfPath = "data.fdf";
        // Output PDF after importing the FDF data
        const string outputPdfPath = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {fdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Export the AcroForm fields of the source PDF to an XML stream
        // ------------------------------------------------------------
        using (Form exporter = new Form())
        {
            // Bind the source PDF to the Form facade
            exporter.BindPdf(inputPdfPath);

            // Export the form fields as XML into a memory stream
            using (MemoryStream xmlStream = new MemoryStream())
            {
                exporter.ExportXml(xmlStream);
                // Reset the stream position if further processing is needed
                xmlStream.Position = 0;

                // (Optional) Save the XML to a file for inspection
                // File.WriteAllBytes("formFields.xml", xmlStream.ToArray());

                // ------------------------------------------------------------
                // Import data from an FDF file into a PDF document
                // ------------------------------------------------------------
                using (Form importer = new Form())
                {
                    // Bind the PDF that will receive the FDF data
                    importer.BindPdf(inputPdfPath);

                    // Open the FDF file as a stream
                    using (FileStream fdfStream = File.OpenRead(fdfPath))
                    {
                        // Import the field values from the FDF stream
                        importer.ImportFdf(fdfStream);
                    }

                    // Save the modified PDF to the desired output location
                    importer.Save(outputPdfPath);
                }
            }
        }
    }
}