using System;
using System.IO;
using System.Text;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputMdPath = "viewer_preferences.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF with the high‑level Document API – this class always exposes ViewerPreferences.
        Document pdfDoc = new Document(inputPdfPath);

        // The ViewerPreferences property exists from Aspose.Pdf 20.9 onward. To keep the code
        // compatible with older versions we access it via reflection and fall back to an empty
        // placeholder when it is not available.
        object prefsObj = null;
        try
        {
            PropertyInfo prefsProp = typeof(Document).GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
            if (prefsProp != null)
                prefsObj = prefsProp.GetValue(pdfDoc);
        }
        catch (Exception ex) when (ex is TargetInvocationException || ex is AmbiguousMatchException)
        {
            // Unexpected reflection failure – treat as not supported.
        }

        if (prefsObj == null)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            // Create an empty anonymous object so the markdown generation loop still runs.
            prefsObj = new { };
        }

        // Build markdown content.
        var md = new StringBuilder();
        md.AppendLine("# PDF Viewer Preferences");
        md.AppendLine();
        md.AppendLine("| Preference | Value |");
        md.AppendLine("|------------|-------|");

        Type prefsType = prefsObj.GetType();
        PropertyInfo[] props = prefsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        if (props.Length == 0)
        {
            md.AppendLine("| (none) | (none) |");
        }
        else
        {
            foreach (var prop in props)
            {
                object value = prop.GetValue(prefsObj, null);
                string valueStr = value?.ToString() ?? "null";
                md.AppendLine($"| {prop.Name} | {valueStr} |");
            }
        }

        // Write markdown to file.
        File.WriteAllText(outputMdPath, md.ToString());

        Console.WriteLine($"Viewer preferences exported to '{outputMdPath}'.");
    }
}
