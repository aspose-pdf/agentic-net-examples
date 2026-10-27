using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputXmlPath = "formdata.xml";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File '{pdfPath}' not found.");
            return;
        }

        // Load the PDF document
        using var doc = new Document(pdfPath);

        // Ensure the PDF contains a form
        if (doc.Form == null || doc.Form.Count == 0)
        {
            Console.Error.WriteLine("The PDF does not contain any form fields.");
            return;
        }

        // -------------------------------------------------------------------
        // 1. Export the form data to JSON (the only export method available on
        //    Aspose.Pdf.Forms.Form in the core API).
        // -------------------------------------------------------------------
        using var jsonStream = new MemoryStream();
        doc.Form.ExportToJson(jsonStream);
        jsonStream.Position = 0; // rewind for reading

        // -------------------------------------------------------------------
        // 2. Convert the JSON payload to a simple XML representation.
        //    The conversion is performed in‑memory – no temporary files are
        //    created – and the resulting XML is written to a file.
        // -------------------------------------------------------------------
        string json;
        using (var reader = new StreamReader(jsonStream, Encoding.UTF8))
            json = reader.ReadToEnd();

        using var jsonDoc = JsonDocument.Parse(json);
        var xmlRoot = new XElement("FormData");
        foreach (var property in jsonDoc.RootElement.EnumerateObject())
        {
            var fieldValue = property.Value.ValueKind switch
            {
                JsonValueKind.String => property.Value.GetString(),
                JsonValueKind.Number => property.Value.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                _ => string.Empty
            };
            xmlRoot.Add(new XElement(property.Name, fieldValue));
        }

        // -------------------------------------------------------------------
        // 3. Write the XML to a file (could be streamed to a response in a web
        //    scenario). The file is UTF‑8 encoded.
        // -------------------------------------------------------------------
        File.WriteAllText(outputXmlPath, xmlRoot.ToString(), Encoding.UTF8);
        Console.WriteLine($"Form data exported to '{outputXmlPath}'.");
    }
}
