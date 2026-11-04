using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for AppearanceDictionary
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "FieldAppearances";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the document actually contains form fields
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the document.");
                    return;
                }

                int fieldIndex = 0;
                foreach (Field field in doc.Form)
                {
                    // Each field may have multiple appearance states.
                    // Export the Normal appearance (most common visual representation).
                    AppearanceDictionary appearances = field.Appearance;
                    if (appearances == null || !appearances.TryGetValue("N", out XForm normalForm))
                    {
                        Console.WriteLine($"Field '{field.Name}' has no normal appearance stream.");
                        fieldIndex++;
                        continue;
                    }

                    // The Normal appearance is a Form XObject. Create a temporary PDF to host it.
                    using (Document appearanceDoc = new Document())
                    {
                        // Determine the size of the appearance XObject via its bounding box.
                        var bbox = normalForm.BBox;
                        double width = bbox.URX - bbox.LLX;
                        double height = bbox.URY - bbox.LLY;

                        // Add a blank page sized to the appearance.
                        Page page = appearanceDoc.Pages.Add();
                        page.PageInfo.Width = width;
                        page.PageInfo.Height = height;

                        // Register the XForm in the page resources.
                        page.Resources.Forms.Add(normalForm);

                        // Draw the XForm onto the page.
                        page.Contents.Add(new GSave());
                        page.Contents.Add(new Do(normalForm.Name));
                        page.Contents.Add(new GRestore());

                        // Build a safe file name for the field.
                        string safeName = string.IsNullOrEmpty(field.Name)
                            ? $"field_{fieldIndex}"
                            : field.Name.Replace(Path.GetInvalidFileNameChars(), '_');

                        string outPath = Path.Combine(outputDir, $"{safeName}_appearance.pdf");

                        // Save the temporary document containing only the appearance stream.
                        appearanceDoc.Save(outPath);
                        Console.WriteLine($"Saved appearance of field '{safeName}' to '{outPath}'.");
                    }

                    fieldIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Extension method to replace invalid filename characters.
static class StringExtensions
{
    public static string Replace(this string str, char[] chars, char replacement)
    {
        foreach (var c in chars)
        {
            str = str.Replace(c, replacement);
        }
        return str;
    }
}