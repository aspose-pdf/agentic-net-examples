using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Locate the first annotation that has a normal appearance (e.g., a Figure‑like annotation)
            Annotation targetAnnot = null;
            foreach (Annotation annot in page.Annotations)
            {
                // Not all annotations expose an appearance; we need one that does.
                if (annot?.Appearance != null)
                {
                    // Use reflection to check for a "NormalAppearance" property at runtime – this avoids compile‑time
                    // dependency on a specific Aspose.Pdf version where the property might be missing.
                    PropertyInfo normalProp = annot.Appearance.GetType().GetProperty("NormalAppearance", BindingFlags.Public | BindingFlags.Instance);
                    if (normalProp != null && normalProp.GetValue(annot.Appearance) != null)
                    {
                        targetAnnot = annot;
                        break;
                    }
                }
            }

            if (targetAnnot == null)
            {
                Console.WriteLine("No annotation with a normal appearance found on the first page.");
                doc.Save(outputPath); // Save unchanged PDF
                return;
            }

            // Retrieve the normal appearance stream via reflection (the type is usually FormXObject, but we treat it as dynamic).
            PropertyInfo normalAppearanceProp = targetAnnot.Appearance.GetType().GetProperty("NormalAppearance", BindingFlags.Public | BindingFlags.Instance);
            if (normalAppearanceProp == null)
            {
                Console.WriteLine("The annotation's appearance dictionary does not expose a NormalAppearance property in this Aspose.Pdf version.");
                doc.Save(outputPath);
                return;
            }

            object normalAppearanceObj = normalAppearanceProp.GetValue(targetAnnot.Appearance);
            if (normalAppearanceObj == null)
            {
                Console.WriteLine("Normal appearance stream is null.");
                doc.Save(outputPath);
                return;
            }

            // The appearance object should expose a "Contents" collection (IList<string>) that holds the PDF content streams.
            PropertyInfo contentsProp = normalAppearanceObj.GetType().GetProperty("Contents", BindingFlags.Public | BindingFlags.Instance);
            if (contentsProp == null)
            {
                Console.WriteLine("Unable to locate Contents collection on the appearance stream.");
                doc.Save(outputPath);
                return;
            }

            // Cast to a generic IList<string> for safe iteration.
            IList<string> contents = contentsProp.GetValue(normalAppearanceObj) as IList<string>;
            if (contents == null)
            {
                Console.WriteLine("Contents collection is not in the expected format.");
                doc.Save(outputPath);
                return;
            }

            // Iterate over each content stream segment and replace RGB fill color operators.
            // Example: replace any "r g b rg" with "1 0 0 rg" (red).
            for (int i = 0; i < contents.Count; i++)
            {
                string originalContent = contents[i];
                string modifiedContent = Regex.Replace(
                    originalContent,
                    @"\b\d+(\.\d+)?\s+\d+(\.\d+)?\s+\d+(\.\d+)?\s+rg\b",
                    "1 0 0 rg");
                contents[i] = modifiedContent;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
