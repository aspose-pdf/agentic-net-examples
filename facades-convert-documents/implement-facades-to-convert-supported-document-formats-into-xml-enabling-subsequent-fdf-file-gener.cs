using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that contains AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Intermediate XML that will hold the exported form data
        const string outputXmlPath = "form_data.xml";

        // Final FDF file generated from the XML
        const string outputFdfPath = "form_data.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF document inside a using block (ensures disposal)
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -------------------------------------------------------------
            // 2. Create a Form facade bound to the loaded document.
            //    The Form facade provides ExportXml() to write form data as XML.
            // -------------------------------------------------------------
            using (Form formFacade = new Form(pdfDoc))
            {
                // Export the form fields to XML
                using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                {
                    formFacade.ExportXml(xmlStream);
                }
            }
        }

        // -------------------------------------------------------------
        // 3. Convert the exported XML into FDF using FormDataConverter.
        //    This static method works with any streams, so we can read
        //    the XML file and write directly to the FDF file.
        // -------------------------------------------------------------
        using (FileStream xmlInput = new FileStream(outputXmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream fdfOutput = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
        {
            FormDataConverter.ConvertXmlToFdf(xmlInput, fdfOutput);
        }

        Console.WriteLine($"XML exported to: {outputXmlPath}");
        Console.WriteLine($"FDF generated at: {outputFdfPath}");
    }
}