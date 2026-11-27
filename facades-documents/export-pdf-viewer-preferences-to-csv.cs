using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvOutputPath = "viewer_preferences.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF using the high‑level Document API (PdfContentEditor does not expose viewer‑preference methods).
        Document pdfDoc = new Document(inputPdfPath);

        // ViewerPreferences was added in later Aspose.Pdf versions. Access it dynamically so the code compiles
        // against older versions as well. If the property is missing we handle the RuntimeBinderException.
        object viewerPrefs;
        try
        {
            dynamic dynDoc = pdfDoc; // cast to dynamic to bypass compile‑time checks
            viewerPrefs = dynDoc.ViewerPreferences;
        }
        catch (RuntimeBinderException)
        {
            Console.Error.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            return;
        }

        // Prepare to write CSV.
        using (StreamWriter writer = new StreamWriter(csvOutputPath, false))
        {
            // CSV header.
            writer.WriteLine("Property,Value");

            // Reflect over all public instance properties of the ViewerPreferences object.
            PropertyInfo[] props = viewerPrefs.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                // Only process boolean properties.
                if (prop.PropertyType == typeof(bool))
                {
                    bool value = (bool)prop.GetValue(viewerPrefs);
                    writer.WriteLine($"{prop.Name},{value}");
                }
            }
        }

        Console.WriteLine($"Viewer preferences exported to '{csvOutputPath}'.");
    }
}
