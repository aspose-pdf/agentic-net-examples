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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Ensure the document contains embedded files
            if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // Locate the ZUGFeRD XML attachment using reflection (avoids direct dependency on EmbeddedFile type)
            object zugferdEmbedded = null;
            foreach (var ef in pdfDoc.EmbeddedFiles)
            {
                var nameProp = ef.GetType().GetProperty("Name");
                if (nameProp == null) continue;
                var name = nameProp.GetValue(ef) as string;
                if (!string.IsNullOrEmpty(name) &&
                    name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                    name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    zugferdEmbedded = ef;
                    break;
                }
            }

            if (zugferdEmbedded == null)
            {
                Console.WriteLine("ZUGFeRD XML attachment not found.");
                return;
            }

            // Try to use the Save(string) method if it exists (Aspose.Pdf 23+ provides it)
            var saveMethod = zugferdEmbedded.GetType().GetMethod("Save", new[] { typeof(string) });
            if (saveMethod != null)
            {
                // Directly save the embedded file to disk
                saveMethod.Invoke(zugferdEmbedded, new object[] { outputXml });
            }
            else
            {
                // Fallback: obtain the underlying stream and copy it manually
                var fileSpecProp = zugferdEmbedded.GetType().GetProperty("FileSpecification");
                var fileSpec = fileSpecProp?.GetValue(zugferdEmbedded);
                var contentsProp = fileSpec?.GetType().GetProperty("Contents");
                var contentsStream = contentsProp?.GetValue(fileSpec) as Stream;
                if (contentsStream != null)
                {
                    using (var outStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                    {
                        contentsStream.CopyTo(outStream);
                    }
                }
                else
                {
                    Console.WriteLine("Unable to extract the ZUGFeRD XML content.");
                    return;
                }
            }

            Console.WriteLine($"ZUGFeRD XML extracted to '{outputXml}'.");
        }
    }
}
