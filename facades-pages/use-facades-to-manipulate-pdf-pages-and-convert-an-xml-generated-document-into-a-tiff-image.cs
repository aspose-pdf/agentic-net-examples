using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class XmlToTiffConverter
{
    static void Main()
    {
        // Paths to the source XML and the resulting TIFF file.
        const string xmlInputPath  = "input.xml";
        const string tiffOutputPath = "output.tiff";

        // Verify that the XML source file exists.
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Error: XML file not found – {xmlInputPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the XML into an Aspose.Pdf.Document.
            //    The Document is created empty and then bound to the XML.
            //    This generates a PDF representation in memory.
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document())
            {
                // BindXml parses the XML and creates the PDF structure.
                pdfDoc.BindXml(xmlInputPath);

                // ------------------------------------------------------------
                // 2. Convert the in‑memory PDF to a single multi‑page TIFF.
                //    PdfConverter is a facade that implements IDisposable,
                //    so it is also wrapped in a using block.
                // ------------------------------------------------------------
                using (PdfConverter converter = new PdfConverter(pdfDoc))
                {
                    // Optional: set a higher resolution for better image quality.
                    // converter.Resolution = 300; // pixels per inch (default is 150)

                    // Prepare the converter for conversion.
                    converter.DoConvert();

                    // Save all pages as one TIFF file.
                    converter.SaveAsTIFF(tiffOutputPath);
                }
            }

            Console.WriteLine($"Successfully converted XML to TIFF: {tiffOutputPath}");
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Pdf or IO exceptions and report them.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}