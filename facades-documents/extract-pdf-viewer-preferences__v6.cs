using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string binPath = "viewerPrefs.bin";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(pdfPath))
        {
            int viewerPrefsValue = 0;
            // Try to obtain the ViewerPreferences property via reflection.
            PropertyInfo viewerPrefsProp = typeof(Document).GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
            if (viewerPrefsProp != null)
            {
                try
                {
                    object value = viewerPrefsProp.GetValue(doc);
                    if (value != null)
                    {
                        // The property is an enum; convert it to its underlying integer value.
                        viewerPrefsValue = Convert.ToInt32(value);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to read ViewerPreferences: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf version. Saving default value 0.");
            }

            // Serialize the integer value to a binary file.
            using (FileStream fs = new FileStream(binPath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(viewerPrefsValue);
            }
        }

        Console.WriteLine($"Viewer preferences saved to '{binPath}'.");
    }
}
