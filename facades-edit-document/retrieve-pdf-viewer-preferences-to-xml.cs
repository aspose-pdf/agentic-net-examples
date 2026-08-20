using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfContentEditor, ViewerPreference
using System.Xml.Linq;            // XDocument, XElement

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // PDF after processing (unchanged)
        const string configXml = "viewer_preferences.xml"; // XML configuration file

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfContentEditor implements IDisposable – use a using block for deterministic cleanup
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Retrieve the current viewer preferences as an integer flag set
            int prefValue = editor.GetViewerPreference();

            // Serialize the preference value to a simple XML file
            XDocument xmlDoc = new XDocument(
                new XElement("ViewerPreferences",
                    new XElement("Preference", prefValue)
                )
            );
            xmlDoc.Save(configXml);

            // Save the (unchanged) PDF to the output path
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Viewer preferences saved to '{configXml}'.");
        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}