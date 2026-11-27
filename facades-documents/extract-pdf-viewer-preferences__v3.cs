using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "metadata.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // PdfContentEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // Retrieve the underlying Document to access ViewerPreferences (may not exist in older versions).
        Document doc = editor.Document;

        // Ensure the Document is disposed after use.
        using (doc)
        {
            // Use dynamic to avoid compile‑time dependency on ViewerPreferences (introduced in later versions).
            dynamic dynDoc = doc;
            Dictionary<string, object> metadataDict = new Dictionary<string, object>();

            try
            {
                // Attempt to get the ViewerPreferences object at runtime.
                var viewerPrefs = dynDoc.ViewerPreferences;

                // If we got it, reflect over its public properties and add them to the dictionary.
                foreach (PropertyInfo pi in viewerPrefs.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    try
                    {
                        object value = pi.GetValue(viewerPrefs);
                        // Convert enum values to their string representation for readability.
                        if (value != null && value.GetType().IsEnum)
                            value = value.ToString();
                        metadataDict[pi.Name] = value;
                    }
                    catch (TargetInvocationException) { /* ignore property that throws */ }
                }
            }
            catch (RuntimeBinderException)
            {
                // ViewerPreferences property is not available in the referenced Aspose.Pdf version.
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Generating empty metadata.");
            }

            // Serialize the metadata dictionary to formatted JSON.
            string json = JsonSerializer.Serialize(metadataDict, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Viewer preferences extracted to '{outputJsonPath}'.");
    }
}
