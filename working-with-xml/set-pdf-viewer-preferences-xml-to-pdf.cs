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

        // Load the XML file as a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // ViewerPreferences were introduced in later versions of Aspose.Pdf.
            // To keep the code compatible with older versions we access the property
            // via dynamic binding and handle the possible RuntimeBinderException.
            dynamic d = doc;
            try
            {
                d.ViewerPreferences.FitWindow = true;                     // Fit the window to the first page size
                d.ViewerPreferences.PageLayout = PageLayout.SinglePage;   // Show one page at a time
                d.ViewerPreferences.HideToolbar = false;                  // Keep the toolbar visible (optional)
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with viewer preferences (if supported): {pdfPath}");
    }
}
