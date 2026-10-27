using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface (may be null if the PDF is not tagged)
            ITaggedContent tagged = doc.TaggedContent;

            // Determine a default language – use a hard‑coded fallback ("en-US")
            // The ITaggedContent interface in older Aspose.Pdf versions does not expose a Language property,
            // so we simply use the default value.
            const string defaultLang = "en-US";

            // If the document already has a language set via the concrete TaggedContent class, preserve it.
            // This is done via reflection to stay compatible with all library versions.
            if (tagged != null)
            {
                var getLangMethod = tagged.GetType().GetMethod("GetLanguage");
                var setLangMethod = tagged.GetType().GetMethod("SetLanguage", new[] { typeof(string) });
                string existingLang = null;
                if (getLangMethod != null)
                {
                    existingLang = getLangMethod.Invoke(tagged, null) as string;
                }
                if (!string.IsNullOrWhiteSpace(existingLang) && setLangMethod != null)
                {
                    // Preserve the existing language if it is already defined.
                    setLangMethod.Invoke(tagged, new object[] { existingLang });
                }
                else if (setLangMethod != null)
                {
                    // Otherwise, set the default language.
                    setLangMethod.Invoke(tagged, new object[] { defaultLang });
                }
            }

            // Get the root structure element (no cast required)
            StructureElement root = tagged?.RootElement;
            if (root == null)
            {
                Console.WriteLine("The document does not contain tagged content.");
                doc.Save(outputPath);
                return;
            }

            // Retrieve all structure elements recursively
            var allElements = root.FindElements<StructureElement>(true);

            // Assign language to each element using the SetLanguage method (available in all supported versions)
            foreach (StructureElement element in allElements)
            {
                var setLang = element.GetType().GetMethod("SetLanguage", new[] { typeof(string) });
                if (setLang != null)
                {
                    setLang.Invoke(element, new object[] { defaultLang });
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
