using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputXml = "filtered_export.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        using (Form form = new Form(inputPdf))
        {
            string[] fieldNames = form.FieldNames;
            using (FileStream fileStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Fields");

                    foreach (string fieldName in fieldNames)
                    {
                        if (fieldName != null && fieldName.StartsWith("Customer", StringComparison.Ordinal))
                        {
                            object fieldValue = form.GetField(fieldName);
                            string valueString = fieldValue != null ? fieldValue.ToString() : string.Empty;

                            writer.WriteStartElement("Field");
                            writer.WriteAttributeString("name", fieldName);
                            writer.WriteString(valueString);
                            writer.WriteEndElement(); // Field
                        }
                    }

                    writer.WriteEndElement(); // Fields
                    writer.WriteEndDocument();
                }
            }
        }

        Console.WriteLine("Filtered form fields exported to " + outputXml);
    }
}