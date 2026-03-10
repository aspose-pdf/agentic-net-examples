using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";          // Input PDF containing ZUGFeRD data
        const string xmlOutputPath = "zugferd.xml";    // Destination for extracted XML

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // The ZugferdInfo property is available only in newer versions of Aspose.Pdf.
            // To keep the code compatible with older versions we access it via dynamic and
            // handle the possible RuntimeBinderException when the member does not exist.
            bool extracted = false;
            try
            {
                dynamic docDyn = pdfDoc; // dynamic bypasses compile‑time checks
                var zugInfo = docDyn.ZugferdInfo; // may throw RuntimeBinderException

                if (zugInfo != null && zugInfo.HasZugferd)
                {
                    string zugferdXml = zugInfo.GetXml();
                    File.WriteAllText(xmlOutputPath, zugferdXml);
                    Console.WriteLine($"ZUGFeRD XML extracted to '{xmlOutputPath}'.");
                    extracted = true;
                }
            }
            catch (RuntimeBinderException)
            {
                // Property not available in the referenced Aspose.Pdf version.
                // You can upgrade the NuGet package to a version that supports ZUGFeRD.
            }

            if (!extracted)
            {
                Console.WriteLine("No ZUGFeRD information found in the PDF or the current Aspose.Pdf version does not support ZugferdInfo.");
            }
        }
    }
}
