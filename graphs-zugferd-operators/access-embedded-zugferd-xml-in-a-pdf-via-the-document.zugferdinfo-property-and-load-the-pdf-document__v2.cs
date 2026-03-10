using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlOutputPath = "zugferd.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Use dynamic to access the ZugferdInfo property without compile‑time binding
            dynamic docDyn = pdfDoc;
            try
            {
                var zugInfo = docDyn.ZugferdInfo;
                if (zugInfo != null)
                {
                    // Assume ZugferdInfo provides a GetXmlStream() method that returns the embedded XML
                    using (Stream xmlStream = zugInfo.GetXmlStream())
                    using (FileStream fileStream = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
                    {
                        xmlStream.CopyTo(fileStream);
                    }
                    Console.WriteLine($"ZUGFeRD XML extracted to '{xmlOutputPath}'.");
                }
                else
                {
                    Console.WriteLine("No ZUGFeRD information found in the PDF.");
                }
            }
            catch (RuntimeBinderException)
            {
                // The current Aspose.Pdf version does not expose ZugferdInfo
                Console.WriteLine("ZugferdInfo property is not available in this Aspose.Pdf version.");
            }
        }
    }
}