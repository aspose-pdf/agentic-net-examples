using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (Facade) which provides GetViewerPreference() in recent versions.
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // The return type of GetViewerPreference() differs between Aspose.Pdf versions:
        //   * Newer versions return Aspose.Pdf.Facades.ViewerPreference
        //   * Older versions return an int (bit‑mask).
        // To compile against any version we retrieve the value via reflection and handle both cases.
        object prefObj = null;
        try
        {
            // Try the strongly‑typed call first – works when the method exists with the expected signature.
            prefObj = editor.GetViewerPreference();
        }
        catch (MissingMethodException)
        {
            // Fallback: invoke the method via reflection to avoid compile‑time binding errors.
            try
            {
                MethodInfo mi = typeof(PdfContentEditor).GetMethod("GetViewerPreference", BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null);
                if (mi != null)
                {
                    prefObj = mi.Invoke(editor, null);
                }
                else
                {
                    Console.WriteLine("GetViewerPreference method not found in the loaded Aspose.Pdf assembly.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error invoking GetViewerPreference via reflection: {ex.Message}");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while retrieving viewer preferences: {ex.Message}");
            return;
        }

        if (prefObj == null)
        {
            Console.WriteLine("Viewer preferences could not be retrieved.");
            return;
        }

        // If the returned object is a ViewerPreference, enumerate its public instance properties.
        if (prefObj is ViewerPreference vp)
        {
            PropertyInfo[] properties = typeof(ViewerPreference).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in properties)
            {
                object value = prop.GetValue(vp);
                string fileName = $"{prop.Name}.txt";
                File.WriteAllText(fileName, value?.ToString() ?? "null");
            }
            Console.WriteLine("Viewer preferences have been saved to individual text files.");
        }
        // If the returned object is an int (old API), just write that integer value to a file.
        else if (prefObj is int intValue)
        {
            string fileName = "ViewerPreferenceInt.txt";
            File.WriteAllText(fileName, intValue.ToString());
            Console.WriteLine("Viewer preference (int) has been saved to ViewerPreferenceInt.txt.");
        }
        else
        {
            Console.WriteLine($"Unsupported viewer preference type: {prefObj.GetType().FullName}");
        }
    }
}
