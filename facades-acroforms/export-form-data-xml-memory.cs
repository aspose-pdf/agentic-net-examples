using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form
        Form pdfForm = new Form(inputPdf);

        // Export form fields to an in‑memory XML stream
        using (MemoryStream xmlStream = new MemoryStream())
        {
            pdfForm.ExportXml(xmlStream);
            // Reset the stream position to read the XML content
            xmlStream.Position = 0;
            using (StreamReader reader = new StreamReader(xmlStream))
            {
                string xmlContent = reader.ReadToEnd();
                Console.WriteLine(xmlContent);
            }
        }
    }
}