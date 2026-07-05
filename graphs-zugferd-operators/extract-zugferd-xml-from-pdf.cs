using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "invoice.pdf";
        const string outputXml = "ZUGFeRD.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Ensure there are embedded files
                if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF.");
                    return;
                }

                // Search for the ZUGFeRD XML attachment using reflection (EmbeddedFile class is in a restricted namespace)
                object xmlEmbeddedFile = null;
                foreach (var embedded in pdfDoc.EmbeddedFiles)
                {
                    var nameProp = embedded.GetType().GetProperty("Name");
                    if (nameProp != null)
                    {
                        var name = nameProp.GetValue(embedded) as string;
                        if (!string.IsNullOrEmpty(name) && name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            xmlEmbeddedFile = embedded;
                            break;
                        }
                    }
                }

                if (xmlEmbeddedFile == null)
                {
                    Console.WriteLine("ZUGFeRD XML attachment not found in the PDF.");
                    return;
                }

                // Try to invoke the Save(string) method directly
                var saveMethod = xmlEmbeddedFile.GetType().GetMethod("Save", new[] { typeof(string) });
                if (saveMethod != null)
                {
                    saveMethod.Invoke(xmlEmbeddedFile, new object[] { outputXml });
                }
                else
                {
                    // Fallback: retrieve the file specification stream and copy its contents
                    var fileSpecProp = xmlEmbeddedFile.GetType().GetProperty("FileSpecification");
                    var fileSpec = fileSpecProp?.GetValue(xmlEmbeddedFile);
                    var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                    var contents = contentsProp?.GetValue(fileSpec) as Stream;
                    if (contents != null)
                    {
                        using (var outStream = File.Create(outputXml))
                        {
                            contents.CopyTo(outStream);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unable to extract the XML content from the embedded file.");
                        return;
                    }
                }

                Console.WriteLine($"ZUGFeRD XML extracted to '{outputXml}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
