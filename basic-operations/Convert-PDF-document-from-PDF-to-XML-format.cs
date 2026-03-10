using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "output.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize XmlSaveOptions for XML export
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                // Save the document as XML using explicit save options
                pdfDoc.Save(outputXml, xmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to XML: '{outputXml}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}