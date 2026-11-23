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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Enable tagging for the document (compatible with multiple Aspose.Pdf versions)
            // -----------------------------------------------------------------
            var tagged = doc.TaggedContent;
            if (tagged != null)
            {
                // Try to call SetTagged(bool) if it exists; otherwise set IsTagged property via reflection
                var setTaggedMethod = tagged.GetType().GetMethod("SetTagged");
                if (setTaggedMethod != null)
                {
                    setTaggedMethod.Invoke(tagged, new object[] { true });
                }
                else
                {
                    var isTaggedProp = tagged.GetType().GetProperty("IsTagged");
                    if (isTaggedProp != null && isTaggedProp.CanWrite)
                    {
                        isTaggedProp.SetValue(tagged, true);
                    }
                }
            }

            // After enabling tagging the root element should be available
            StructureElement rootElement = doc.TaggedContent.RootElement;
            if (rootElement == null)
            {
                // As a fallback, create a section element and treat it as the root.
                // This scenario is rare but ensures the code works even if the library
                // does not auto‑create a root element.
                var sect = doc.TaggedContent.CreateSectElement();
                // The RootElement property is read‑only; the newly created element becomes
                // the logical root automatically when added to the document's structure.
                // Append it to the document's structure tree.
                // Since there is no direct setter, we simply use it as the parent for further inserts.
                rootElement = sect;
            }

            // Create a new paragraph element to insert
            ParagraphElement paragraph = doc.TaggedContent.CreateParagraphElement();
            paragraph.SetText("This paragraph was inserted using InsertChild.");

            // Insert the new paragraph as the first child of the root element
            // InsertChild takes the element to insert and the zero‑based index position
            rootElement.InsertChild(paragraph, 0);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with inserted structure element to '{outputPath}'.");
    }
}
