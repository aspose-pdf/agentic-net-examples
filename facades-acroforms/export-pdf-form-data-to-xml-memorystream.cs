using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document and bind it to the Form facade
        using (Document doc = new Document(pdfPath))
        using (Form formFacade = new Form(doc))
        using (MemoryStream xmlStream = new MemoryStream())
        {
            // Export form data to XML via the facade (no intermediate file)
            formFacade.ExportXml(xmlStream);

            // Reset the stream position to read the XML content
            xmlStream.Position = 0;
            using (StreamReader reader = new StreamReader(xmlStream))
            {
                string xmlContent = reader.ReadToEnd();
                Console.WriteLine("Exported XML:");
                Console.WriteLine(xmlContent);
            }
        }
    }
}
