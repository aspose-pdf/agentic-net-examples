using System;
using System.IO;
using System.Text;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputCsv = "annotations_audit.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("PageNumber,AnnotationType,LLX,LLY,URX,URY,Title,Contents");

                    // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];
                        // Each page has an Annotations collection
                        foreach (Annotation annot in page.Annotations)
                        {
                            // Basic properties
                            string typeName = annot.GetType().Name;
                            Aspose.Pdf.Rectangle rect = annot.Rect;

                            // Optional Title and Contents – use reflection to avoid missing members
                            string title = GetPropertyValue<string>(annot, "Title");
                            string contents = GetPropertyValue<string>(annot, "Contents");

                            // Escape fields for CSV (wrap in quotes and double any internal quotes)
                            string Escape(string s) => $"\"{s?.Replace("\"", "\"\"")}\"";

                            // Write CSV line
                            writer.WriteLine(
                                $"{pageNum}," +
                                $"{Escape(typeName)}," +
                                $"{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}," +
                                $"{Escape(title)}," +
                                $"{Escape(contents)}");
                        }
                    }
                }

                Console.WriteLine($"Annotation audit CSV saved to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to safely read a property via reflection; returns default(T) if not present
    private static T GetPropertyValue<T>(object obj, string propertyName)
    {
        PropertyInfo prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.PropertyType == typeof(T))
        {
            return (T)prop.GetValue(obj);
        }
        return default;
    }
}