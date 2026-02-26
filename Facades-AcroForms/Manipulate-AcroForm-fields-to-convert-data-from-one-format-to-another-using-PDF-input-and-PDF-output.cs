using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePdf = "template.pdf";   // PDF with AcroForm fields
        const string xmlData     = "data.xml";      // Source data in XML format
        const string outputPdf   = "filled.pdf";    // Resulting PDF after conversion

        // Verify that required files exist
        if (!File.Exists(templatePdf) || !File.Exists(xmlData))
        {
            Console.Error.WriteLine("Input PDF or XML file not found.");
            return;
        }

        // Step 1: Convert the XML data to an FDF stream (in‑memory)
        using (FileStream xmlStream = File.OpenRead(xmlData))
        using (MemoryStream fdfStream = new MemoryStream())
        {
            // ConvertXmlToFdf creates an FDF representation of the XML form data
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            fdfStream.Position = 0; // Reset stream position for reading

            // Step 2: Load the PDF form and import the FDF data
            using (Document pdfDoc = new Document(templatePdf))
            {
                // Form facade works directly on the Document instance
                Form form = new Form(pdfDoc);
                form.ImportFdf(fdfStream);   // Populate the AcroForm fields

                // Save the updated PDF
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"AcroForm fields populated and saved to '{outputPdf}'.");
    }
}