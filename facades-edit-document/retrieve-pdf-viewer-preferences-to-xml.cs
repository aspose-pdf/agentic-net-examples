using System;
using System.IO;
using System.Xml.Serialization;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder; // for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "viewerPreferences.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // PdfFileInfo implements IDisposable, so wrap it in a using block.
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
            {
                // Attempt to retrieve ViewerPreferences via dynamic access.
                // This avoids a compile‑time dependency on the ViewerPreferences type,
                // which may not exist in older Aspose.Pdf versions.
                object preferencesObj = null;
                Type preferencesType = null;
                try
                {
                    dynamic dynInfo = pdfInfo;
                    preferencesObj = dynInfo.ViewerPreferences; // may throw RuntimeBinderException
                    preferencesType = preferencesObj?.GetType();
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
                }

                if (preferencesObj != null && preferencesType != null)
                {
                    // Serialize the ViewerPreferences object to XML using its runtime type.
                    XmlSerializer serializer = new XmlSerializer(preferencesType);
                    using (FileStream fs = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                    {
                        serializer.Serialize(fs, preferencesObj);
                    }
                    Console.WriteLine($"Viewer preferences saved to '{outputXmlPath}'.");
                }
                else
                {
                    // Fallback: create an empty file or skip serialization.
                    File.WriteAllText(outputXmlPath, "<!-- ViewerPreferences not available -->");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
