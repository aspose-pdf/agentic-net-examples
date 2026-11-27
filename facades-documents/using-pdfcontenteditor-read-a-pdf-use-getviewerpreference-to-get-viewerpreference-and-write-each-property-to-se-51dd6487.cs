using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Aspose.Pdf.Facades; // PdfContentEditor and ViewerPreference are here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does NOT implement IDisposable – do NOT wrap in using
        PdfContentEditor editor = new PdfContentEditor();
        // Bind the PDF first – required before calling GetViewerPreference
        editor.BindPdf(inputPath);

        // Retrieve the viewer preference bit‑mask from the PDF
        int prefMask = editor.GetViewerPreference();

        // Directory to store the JSON files
        string outputDir = "ViewerPreferences";
        Directory.CreateDirectory(outputDir);

        // Helper to write a single property to its own JSON file
        void WriteJson<T>(string propName, T value)
        {
            var payload = new { Property = propName, Value = value };
            string json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path.Combine(outputDir, $"{propName}.json"), json);
        }

        // Write the raw integer mask (useful for debugging)
        WriteJson(nameof(prefMask), prefMask);

        // ViewerPreference is a static class that only contains flag constants (int values).
        // Iterate over all public static int fields and write whether each flag is set.
        foreach (FieldInfo field in typeof(ViewerPreference).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.FieldType != typeof(int))
                continue;

            int flagValue = (int)field.GetValue(null);
            bool isSet = (prefMask & flagValue) != 0;
            WriteJson(field.Name, isSet);
        }

        Console.WriteLine($"Viewer preferences have been written to folder: {outputDir}");
    }
}
