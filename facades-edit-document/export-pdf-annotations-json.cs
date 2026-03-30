using System;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonPath = "annotations.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xfdfStream);
                string xfdfXml = xmlDoc.OuterXml;

                Dictionary<string, string> jsonObject = new Dictionary<string, string>
                {
                    { "xfdf", xfdfXml }
                };

                using (FileStream jsonFs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    JsonSerializer.Serialize(jsonFs, jsonObject, options);
                }
            }
        }

        Console.WriteLine($"Annotations exported to '{jsonPath}'.");
    }
}
