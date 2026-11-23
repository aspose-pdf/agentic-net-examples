using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_text.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not tagged; no tagged text to extract.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;
            // Use dynamic to avoid compile‑time dependency on StructureElement which may be absent in older SDK versions
            dynamic root = tagged.RootElement;

            var sb = new StringBuilder();
            CollectActualText(root, sb);

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Tagged text extracted to '{outputPath}'.");
        }
    }

    private static void CollectActualText(dynamic element, StringBuilder sb)
    {
        if (element == null) return;

        // Try to read the ActualText property; ignore if it does not exist on this version
        try
        {
            string text = element.ActualText as string;
            if (!string.IsNullOrWhiteSpace(text))
                sb.AppendLine(text.Trim());
        }
        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }

        // Retrieve child elements – the SDK may expose them via different property names
        IEnumerable<dynamic> children = null;
        try { children = element.ChildElements as IEnumerable<dynamic>; } catch { }
        if (children == null)
        {
            try { children = element.Children as IEnumerable<dynamic>; } catch { }
        }
        if (children == null)
        {
            try { children = element.Elements as IEnumerable<dynamic>; } catch { }
        }

        if (children != null)
        {
            foreach (var child in children)
                CollectActualText(child, sb);
        }
    }
}