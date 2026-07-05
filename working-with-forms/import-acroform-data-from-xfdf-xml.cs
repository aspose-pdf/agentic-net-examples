using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string xmlDataPath    = "updatedData.xml";
        const string outputPdfPath  = "output.pdf";

        // Verify files exist
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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the XFDF/XML stream containing form field values
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    // Import field values from the XFDF (XML) into the PDF's AcroForm
                    XfdfReader.ReadFields(xmlStream, pdfDoc);
                }

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Form data imported successfully. Saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}