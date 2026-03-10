using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document src = new Document(inputPath))
            {
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    using (Document single = new Document())
                    {
                        // Copy the standard metadata fields (Title, Author, etc.).
                        // Custom properties are optional and are only copied when the
                        // DocumentInfo.CustomProperties member is available in the
                        // referenced Aspose.Pdf version.
                        CopyMetadata(src, single);

                        // Add the current page (Aspose clones the page internally).
                        single.Pages.Add(src.Pages[i]);

                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        single.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void CopyMetadata(Document source, Document target)
    {
        // PdfFileInfo objects are read‑only, but their individual members are writable.
        var srcInfo = source.Info;
        var tgtInfo = target.Info;

        tgtInfo.Title = srcInfo.Title;
        tgtInfo.Author = srcInfo.Author;
        tgtInfo.Subject = srcInfo.Subject;
        tgtInfo.Keywords = srcInfo.Keywords;
        tgtInfo.Creator = srcInfo.Creator;
        // Producer is read‑only; it will be set automatically by Aspose.

        // ----- Custom properties handling (version‑safe) -----
        // Older Aspose.Pdf releases do not expose DocumentInfo.CustomProperties.
        // To keep the code compatible we attempt to copy them via reflection.
        // If the property is not present the operation is silently skipped.
        try
        {
            var customPropsProp = srcInfo.GetType().GetProperty("CustomProperties");
            var customPropsTarget = tgtInfo.GetType().GetProperty("CustomProperties");
            if (customPropsProp != null && customPropsTarget != null)
            {
                var srcCustom = customPropsProp.GetValue(srcInfo);
                var tgtCustom = customPropsTarget.GetValue(tgtInfo);
                // Both source and target are expected to implement IEnumerable<KeyValuePair<string,string>>
                // and have an Add(string, string) method.
                var addMethod = tgtCustom.GetType().GetMethod("Add", new[] { typeof(string), typeof(string) });
                var enumerator = ((System.Collections.IEnumerable)srcCustom).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var entry = enumerator.Current; // entry is KeyValuePair<string,string>
                    var keyProp = entry.GetType().GetProperty("Key");
                    var valueProp = entry.GetType().GetProperty("Value");
                    string key = (string)keyProp.GetValue(entry);
                    string value = (string)valueProp.GetValue(entry);
                    addMethod.Invoke(tgtCustom, new object[] { key, value });
                }
            }
        }
        catch
        {
            // If reflection fails (property missing, different type, etc.) we simply ignore custom properties.
        }
    }
}
