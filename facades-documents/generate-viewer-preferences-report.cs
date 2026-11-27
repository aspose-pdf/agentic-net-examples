using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";
        // Output plain‑text report
        const string reportPath = "viewer_preferences_report.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using the high‑level API (Document)
        Document pdfDoc = new Document(inputPdfPath);

        // Try to obtain ViewerPreferences via dynamic access – works on newer versions
        // and falls back gracefully on older versions where the property does not exist.
        object prefs = null;
        try
        {
            dynamic dynDoc = pdfDoc;
            prefs = dynDoc.ViewerPreferences; // May throw RuntimeBinderException if missing
        }
        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }

        // Write a formatted report
        using (StreamWriter writer = new StreamWriter(reportPath))
        {
            writer.WriteLine($"Viewer Preferences Report for \"{Path.GetFileName(inputPdfPath)}\"");
            writer.WriteLine(new string('=', 60));
            writer.WriteLine();

            if (prefs != null)
            {
                // Use reflection to list all public instance properties of the ViewerPreferences object
                PropertyInfo[] properties = prefs.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in properties)
                {
                    object value = prop.GetValue(prefs);
                    writer.WriteLine($"{prop.Name}: {value}");
                }
            }
            else
            {
                writer.WriteLine("ViewerPreferences information is unavailable for this PDF or library version.");
            }
        }

        Console.WriteLine($"Report generated: {reportPath}");
    }
}
