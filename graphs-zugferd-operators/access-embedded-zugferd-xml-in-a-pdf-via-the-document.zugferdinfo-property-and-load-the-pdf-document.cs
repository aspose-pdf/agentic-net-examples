using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the standard constructor (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Try to obtain ZUGFeRD information via the newer ZugferdInfo property (if the library version supports it)
            var zugInfo = GetZugferdInfo(doc);
            if (zugInfo != null)
            {
                // The ZugferdInfo object (when present) provides a GetXmlStream() method.
                using (Stream xmlStream = zugInfo.GetXmlStream())
                {
                    PrintXml(xmlStream);
                }
                return;
            }

            // Fallback for older Aspose.Pdf versions: search embedded files for a ZUGFeRD XML attachment.
            using (Stream xmlStream = FindZugferdXmlInEmbeddedFiles(doc))
            {
                if (xmlStream != null)
                {
                    PrintXml(xmlStream);
                    return;
                }
            }

            Console.WriteLine("No ZUGFeRD data embedded in this PDF.");
        }
    }

    // Uses reflection to safely access the ZugferdInfo property when it exists.
    private static dynamic GetZugferdInfo(Document doc)
    {
        PropertyInfo prop = typeof(Document).GetProperty("ZugferdInfo", BindingFlags.Public | BindingFlags.Instance);
        return prop?.GetValue(doc);
    }

    // Searches the EmbeddedFiles collection for an XML file whose name contains "zugferd".
    private static Stream FindZugferdXmlInEmbeddedFiles(Document doc)
    {
        // In recent Aspose.Pdf versions the collection holds FileSpecification objects, not EmbeddedFile.
        if (doc.EmbeddedFiles == null)
            return null;

        foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
        {
            if (fileSpec.Name != null &&
                fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                fileSpec.Name.IndexOf("zugferd", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Copy the embedded file into a memory stream so the caller can read it.
                MemoryStream ms = new MemoryStream();
                // The FileSpecification class does not expose a Save method; use the Contents stream instead.
                fileSpec.Contents.CopyTo(ms);
                ms.Position = 0;
                return ms;
            }
        }
        return null;
    }

    // Reads the XML stream and writes its content to the console.
    private static void PrintXml(Stream xmlStream)
    {
        if (xmlStream == null)
        {
            Console.WriteLine("ZUGFeRD XML stream is empty.");
            return;
        }

        using (StreamReader reader = new StreamReader(xmlStream))
        {
            string xmlContent = reader.ReadToEnd();
            Console.WriteLine("Embedded ZUGFeRD XML:");
            Console.WriteLine(xmlContent);
        }
    }
}
