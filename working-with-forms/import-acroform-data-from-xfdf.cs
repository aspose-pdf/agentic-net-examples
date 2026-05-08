using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmlDataPath   = "updatedData.xml";
        const string outputPdfPath = "output.pdf";

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
            // Load the PDF document containing the AcroForm
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the XFDF (XML) file and import its field values into the PDF
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    XfdfReader.ReadFields(xmlStream, pdfDoc);
                }

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}