using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "output.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF using PdfContentEditor as required
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        // Default values in case the properties are unavailable
        bool centerWindow = false;
        bool fitWindow = false;

        // Use reflection to read ViewerPreferences without a compile‑time dependency
        try
        {
            using (Document doc = new Document(pdfPath))
            {
                PropertyInfo vpProp = doc.GetType().GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
                if (vpProp != null)
                {
                    object vp = vpProp.GetValue(doc);
                    if (vp != null)
                    {
                        PropertyInfo centerProp = vp.GetType().GetProperty("CenterWindow", BindingFlags.Public | BindingFlags.Instance);
                        PropertyInfo fitProp = vp.GetType().GetProperty("FitWindow", BindingFlags.Public | BindingFlags.Instance);

                        if (centerProp != null && centerProp.PropertyType == typeof(bool))
                            centerWindow = (bool)centerProp.GetValue(vp);

                        if (fitProp != null && fitProp.PropertyType == typeof(bool))
                            fitWindow = (bool)fitProp.GetValue(vp);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading viewer preferences: {ex.Message}");
        }

        // Write a single CSV line with the two boolean values
        try
        {
            string csvLine = $"{centerWindow},{fitWindow}";
            File.WriteAllText(csvPath, csvLine);
            Console.WriteLine($"CSV written to '{csvPath}': {csvLine}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSV: {ex.Message}");
        }
    }
}