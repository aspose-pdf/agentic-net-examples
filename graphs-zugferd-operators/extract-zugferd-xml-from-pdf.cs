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
            using (Document doc = new Document(inputPdf))
            {
                // Access embedded files collection
                EmbeddedFileCollection attachments = doc.EmbeddedFiles;
                if (attachments == null || attachments.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF.");
                    return;
                }

                FileSpecification zugferdSpec = null;

                // Search for the ZUGFeRD XML attachment (by name or MIME type)
                for (int i = 1; i <= attachments.Count; i++) // EmbeddedFileCollection uses 1‑based indexing
                {
                    FileSpecification spec = attachments[i];
                    if (!string.IsNullOrEmpty(spec.Name) &&
                        spec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                        (spec.Name.IndexOf("zugferd", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         (spec.MIMEType != null && spec.MIMEType.Contains("xml"))))
                    {
                        zugferdSpec = spec;
                        break;
                    }
                }

                if (zugferdSpec == null)
                {
                    Console.WriteLine("ZUGFeRD XML attachment not found.");
                    return;
                }

                // Extract the XML content and save it to a file
                using (Stream src = zugferdSpec.Contents)
                using (FileStream dest = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                {
                    src.CopyTo(dest);
                }

                Console.WriteLine($"ZUGFeRD XML extracted and saved to '{outputXml}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}