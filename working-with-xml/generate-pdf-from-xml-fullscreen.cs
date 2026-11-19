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

        // Load the XML file into a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Enable full‑screen viewer mode if the current Aspose.Pdf version supports it.
            // We avoid compile‑time references to ViewerPreferences/PageModeEnum because older
            // library versions may not contain them. Instead we use dynamic + reflection.
            dynamic dynDoc = doc;
            try
            {
                // Obtain the ViewerPreferences object (may throw if the property does not exist).
                dynamic prefs = dynDoc.ViewerPreferences;

                // Resolve the nested enum type "Aspose.Pdf.ViewerPreferences+PageModeEnum" at runtime.
                Type enumType = Type.GetType("Aspose.Pdf.ViewerPreferences+PageModeEnum, Aspose.Pdf");
                if (enumType != null)
                {
                    // Parse the enum value "FullScreen".
                    object fullScreenValue = Enum.Parse(enumType, "FullScreen");
                    // Assign the enum value to the PageMode property.
                    prefs.PageMode = fullScreenValue;
                }
                else
                {
                    Console.WriteLine("ViewerPreferences.PageModeEnum type not found – full‑screen mode cannot be set.");
                }
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Full‑screen mode will not be set.");
            }

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with full‑screen mode (if supported): {pdfPath}");
    }
}
