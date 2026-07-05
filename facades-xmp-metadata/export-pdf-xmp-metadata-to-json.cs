using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "xmp_metadata.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and extract its XMP metadata using the PdfXmpMetadata facade.
        using (Document pdfDoc = new Document(inputPdfPath))
        using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
        {
            // Bind the facade to the loaded document.
            xmpFacade.BindPdf(pdfDoc);

            // Retrieve the XMP metadata as XML bytes.
            byte[] xmpXmlBytes = xmpFacade.GetXmpMetadata();

            // Convert the XML bytes to a UTF‑8 string.
            string xmpXml = Encoding.UTF8.GetString(xmpXmlBytes);

            // Wrap the XML string in a simple JSON object.
            var jsonObject = new { xmp = xmpXml };
            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON representation to the output file.
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"XMP metadata exported to JSON file: {outputJsonPath}");
    }
}