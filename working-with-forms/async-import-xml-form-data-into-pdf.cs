using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static async Task Main()
    {
        const string pdfPath = "template.pdf";
        const string xmlDataPath = "formdata.xml";
        const string outputPath = "filled.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlDataPath}");
            return;
        }

        await ImportXmlFormDataAsync(pdfPath, xmlDataPath, outputPath);
        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }

    // Asynchronously loads the XML, maps its elements to form fields and saves the PDF.
    static async Task ImportXmlFormDataAsync(string pdfFile, string xmlFile, string outputFile)
    {
        // Load the PDF document.
        using var doc = new Document(pdfFile);

        // Load the XML document asynchronously.
        await using var xmlStream = File.OpenRead(xmlFile);
        // Disambiguate LoadOptions – we need the one from System.Xml.Linq.
        var xDoc = await XDocument.LoadAsync(xmlStream, System.Xml.Linq.LoadOptions.None, default);

        // Simple convention: each direct child element of the root represents a form field.
        // Element name = field name, element value = field value.
        if (doc.Form?.Fields != null && xDoc.Root != null)
        {
            foreach (var element in xDoc.Root.Elements())
            {
                string fieldName = element.Name.LocalName;
                string fieldValue = element.Value ?? string.Empty;

                var field = doc.Form.Fields.FirstOrDefault(f => f.Name == fieldName);
                if (field != null)
                {
                    field.Value = fieldValue;
                }
            }
        }

        // Save the updated PDF.
        doc.Save(outputFile);
    }
}
