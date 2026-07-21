using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";          // source PDF with AcroForm
        const string xmlDataPath  = "updatedData.xml";    // XFDF/XML containing form data
        const string outputPath   = "output.pdf";         // PDF after data import

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Open the XFDF/XML stream and import field values into the document
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    // Core API method that reads field values from an XFDF (XML) stream
                    XfdfReader.ReadFields(xmlStream, doc);
                }

                // Save the updated PDF
                doc.Save(outputPath);
                Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}