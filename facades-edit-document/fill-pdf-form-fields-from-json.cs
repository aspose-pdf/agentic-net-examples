using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string jsonDataPath    = "data.json";
        const string outputPdfPath   = "filled.pdf";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load JSON mapping field names -> values
        string jsonContent = File.ReadAllText(jsonDataPath);
        var fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent) ??
                          new Dictionary<string, string>();

        // Build XFDF (XML Forms Data Format) from the JSON data
        StringBuilder xfdfBuilder = new StringBuilder();
        xfdfBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        xfdfBuilder.AppendLine("<xfdf xmlns=\"http://ns.adobe.com/xfdf/\" xml:space=\"preserve\">");
        xfdfBuilder.AppendLine("<fields>");
        foreach (var kvp in fieldValues)
        {
            // Escape XML special characters
            string name  = System.Security.SecurityElement.Escape(kvp.Key);
            string value = System.Security.SecurityElement.Escape(kvp.Value);
            xfdfBuilder.AppendLine($"  <field name=\"{name}\"><value>{value}</value></field>");
        }
        xfdfBuilder.AppendLine("</fields>");
        xfdfBuilder.AppendLine("</xfdf>");

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Use the Form facade to import XFDF data and save the filled PDF
            using (Form formFacade = new Form(pdfDoc))
            {
                // Import XFDF – the facade provides ImportXfdf for XFDF data
                using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfBuilder.ToString())))
                {
                    formFacade.ImportXfdf(xfdfStream);
                }

                // Save the result
                formFacade.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form fields have been filled and saved to '{outputPdfPath}'.");
    }
}
