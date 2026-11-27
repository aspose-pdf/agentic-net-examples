using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder; // needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string pdfPath = "source.pdf";
        const string outPath = "preferences.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // 1. Create a PdfContentEditor and bind the PDF (required by the task description).
        // PdfContentEditor does NOT implement IDisposable – do NOT wrap in using.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        // 2. Load the same PDF with Document. The ViewerPreferences property exists only in newer
        //    versions of Aspose.Pdf. To keep the code compilable with older versions we access it
        //    via dynamic and handle the possible RuntimeBinderException.
        Document doc = new Document(pdfPath);
        object viewerPrefs = null;
        try
        {
            // Dynamic access bypasses compile‑time member checking.
            dynamic dynDoc = doc;
            viewerPrefs = dynDoc.ViewerPreferences;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }

        // 3. Write the preferences (if any) to a text file.
        using (StreamWriter writer = new StreamWriter(outPath, false))
        {
            if (viewerPrefs != null)
            {
                Type prefsType = viewerPrefs.GetType();
                PropertyInfo[] props = prefsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in props)
                {
                    object value = prop.GetValue(viewerPrefs, null);
                    writer.WriteLine($"{prop.Name} = {value}");
                }
            }
            else
            {
                writer.WriteLine("ViewerPreferences not available in this Aspose.Pdf version.");
            }
        }

        Console.WriteLine($"Viewer preferences written to '{outPath}'.");
    }
}
