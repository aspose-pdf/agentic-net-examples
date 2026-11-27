using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string reportTxt = "viewer_preferences_report.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF with the high‑level Document API.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Try to obtain the ViewerPreferences property via reflection.
                PropertyInfo vpProp = pdfDoc.GetType().GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);

                // Build a plain‑text report.
                using (StreamWriter writer = new StreamWriter(reportTxt, false))
                {
                    writer.WriteLine("Viewer Preferences Report");
                    writer.WriteLine($"Source PDF: {Path.GetFileName(inputPdf)}");
                    writer.WriteLine($"Generated on: {DateTime.UtcNow:u}");
                    writer.WriteLine(new string('=', 40));
                    writer.WriteLine();

                    if (vpProp != null)
                    {
                        // The property exists – retrieve its value and list its public properties.
                        object preferences = vpProp.GetValue(pdfDoc);
                        if (preferences != null)
                        {
                            PropertyInfo[] prefProps = preferences.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            foreach (PropertyInfo prop in prefProps)
                            {
                                object value = prop.GetValue(preferences, null);
                                writer.WriteLine($"{prop.Name}: {value}");
                            }
                        }
                        else
                        {
                            writer.WriteLine("ViewerPreferences property is present but returned null.");
                        }
                    }
                    else
                    {
                        // The property does not exist in the current Aspose.Pdf version.
                        writer.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
                    }
                }
            }

            Console.WriteLine($"Viewer preferences report saved to '{reportTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
