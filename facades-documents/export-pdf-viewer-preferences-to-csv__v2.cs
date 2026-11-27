using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;                     // Document API
using Aspose.Pdf.Facades;               // PdfContentEditor lives here
using Microsoft.CSharp.RuntimeBinder;   // RuntimeBinderException for dynamic access

class Program
{
    static void Main()
    {
        // Input PDF and output CSV paths
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "viewer_preferences.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF using PdfContentEditor (the editor is only needed to demonstrate the API usage)
        // It implements IDisposable in newer versions, so we wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            // No further operations with the editor are required for this task.
        }

        // Retrieve viewer preferences via dynamic access.
        // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
        // Using dynamic bypasses compile‑time checks and allows the code to compile against older versions.
        object prefsObj = null;
        try
        {
            dynamic docDynamic = new Document(inputPdfPath);
            prefsObj = docDynamic.ViewerPreferences; // May throw RuntimeBinderException if property is missing
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while accessing ViewerPreferences: {ex.Message}");
        }

        // If preferences were obtained, reflect over its boolean properties and write them to CSV.
        using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
        {
            writer.WriteLine("Property,Value");

            if (prefsObj != null)
            {
                Type prefsType = prefsObj.GetType();
                PropertyInfo[] properties = prefsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in properties)
                {
                    if (prop.PropertyType == typeof(bool))
                    {
                        bool value = (bool)prop.GetValue(prefsObj);
                        writer.WriteLine($"{prop.Name},{value}");
                    }
                }
            }
            else
            {
                // No preferences available – write a placeholder line.
                writer.WriteLine("NoViewerPreferencesAvailable,False");
            }
        }

        Console.WriteLine($"Viewer preferences exported to '{outputCsvPath}'.");
    }
}
