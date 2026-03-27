using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputXfdf = "annotations.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Export annotations to a memory stream
            using (MemoryStream memory = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(memory);
                memory.Position = 0;

                // Load the raw XFDF XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(memory);

                // Save with indentation (pretty‑print)
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.Encoding = System.Text.Encoding.UTF8;

                using (XmlWriter writer = XmlWriter.Create(outputXfdf, settings))
                {
                    xmlDoc.Save(writer);
                }
            }
        }

        Console.WriteLine($"Annotations exported to '{outputXfdf}' with pretty formatting.");
    }
}