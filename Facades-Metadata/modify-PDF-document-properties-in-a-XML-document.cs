using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the XML metadata file and the output PDF.
        const string pdfPath = "input.pdf";
        const string xmlMetaPath = "metadata.xml";
        const string outputPdf = "output.pdf";

        // Validate input files.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlMetaPath))
        {
            Console.Error.WriteLine($"XML metadata file not found: {xmlMetaPath}");
            return;
        }

        // Load the XML file. Expected format:
        // <Metadata>
        //   <Entry name="Title">My Document</Entry>
        //   <Entry name="Author">John Doe</Entry>
        //   ...
        // </Metadata>
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlMetaPath);

        // Use PdfFileInfo facade to modify document properties.
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the existing PDF file.
            pdfInfo.BindPdf(pdfPath);

            // Iterate over each <Entry> node and apply the metadata.
            XmlNodeList entries = xmlDoc.SelectNodes("//Entry");
            if (entries != null)
            {
                foreach (XmlNode entry in entries)
                {
                    XmlAttribute nameAttr = entry.Attributes["name"];
                    if (nameAttr == null) continue; // Skip malformed entries.

                    string key = nameAttr.Value;
                    string value = entry.InnerText ?? string.Empty;

                    // Map well‑known keys to strongly‑typed properties; otherwise use generic SetMetaInfo.
                    switch (key.ToLowerInvariant())
                    {
                        case "title":
                            pdfInfo.Title = value;
                            break;
                        case "author":
                            pdfInfo.Author = value;
                            break;
                        case "subject":
                            pdfInfo.Subject = value;
                            break;
                        case "keywords":
                            pdfInfo.Keywords = value;
                            break;
                        case "creator":
                            pdfInfo.Creator = value;
                            break;
                        // Producer is read‑only via property; use generic setter if needed.
                        case "producer":
                            pdfInfo.SetMetaInfo("Producer", value);
                            break;
                        default:
                            pdfInfo.SetMetaInfo(key, value);
                            break;
                    }
                }
            }

            // Save the updated PDF to a new file. SaveNewInfo writes only the modified
            // document information without re‑creating the whole PDF.
            pdfInfo.SaveNewInfo(outputPdf);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}