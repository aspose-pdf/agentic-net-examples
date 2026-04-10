using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";          // PDF with AcroForm fields
        const string xmlDataPath  = "updatedData.xml";       // XFDF‑style XML containing field values
        const string outputPdfPath = "filled.pdf";

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
            // Load the PDF document that contains the form
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the XFDF (XML) stream
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    // Import field values from the XFDF stream into the PDF document
                    XfdfReader.ReadFields(xmlStream, pdfDoc);
                }

                // Save the updated PDF with imported form data
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}