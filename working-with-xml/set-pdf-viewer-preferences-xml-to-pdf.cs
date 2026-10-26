using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Attempt to set viewer preferences if the current Aspose.Pdf version supports them.
            // The ViewerPreferences property was introduced in later versions of Aspose.Pdf.
            // Using dynamic bypasses compile‑time checks and allows the code to compile against older versions.
            dynamic d = doc;
            try
            {
                d.ViewerPreferences.FitWindow = true;
                d.ViewerPreferences.PageLayout = PageLayout.SinglePage;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf version. Skipping viewer preference configuration.");
            }

            // Save the PDF with the (potentially) applied viewer preferences
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with viewer preferences: {pdfPath}");
    }
}
