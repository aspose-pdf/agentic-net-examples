using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Iterate over each image resource on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Retrieve the alternative text using reflection (property may not exist in older versions)
                        string altText = GetAlternativeText(img) ?? "(no alt text)";

                        // Log the alt text for auditing
                        Console.WriteLine($"Page {i}: Image Alt Text = '{altText}'");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Attempts to obtain the AlternativeText property from an XImage instance.
    /// Returns null if the property does not exist or its value is null.
    /// </summary>
    private static string GetAlternativeText(XImage img)
    {
        if (img == null)
            return null;

        // Use reflection to avoid compile‑time dependency on a property that may be absent in some library versions.
        PropertyInfo altProp = img.GetType().GetProperty("AlternativeText", BindingFlags.Public | BindingFlags.Instance);
        if (altProp != null)
        {
            object value = altProp.GetValue(img);
            return value as string;
        }
        return null;
    }
}