using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "paragraph_child_ids.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document contains tagged content
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not a tagged PDF.");
                return;
            }

            // Access the tagged‑content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root element of the structure tree (use dynamic to avoid version‑specific types)
            dynamic root = taggedContent.RootElement;

            // Find the first paragraph element (tag "P") in a depth‑first walk
            dynamic paragraph = FindFirstParagraph(root);
            if (paragraph == null)
            {
                Console.WriteLine("No paragraph element found in the document.");
                return;
            }

            // Retrieve the child elements of the paragraph
            var childElements = paragraph.ChildElements;

            // Collect the IDs of each child element (StructureElement provides GetId())
            List<string> childIds = new List<string>();
            foreach (dynamic child in childElements)
            {
                // All logical‑structure elements inherit from StructureElement, which exposes GetId()
                try
                {
                    string id = child.GetId();
                    if (!string.IsNullOrEmpty(id))
                        childIds.Add(id);
                }
                catch
                {
                    // If the element does not expose GetId (unlikely), just skip it
                }
            }

            // Serialize the list of IDs to JSON
            string json = JsonSerializer.Serialize(childIds, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputJson, json);

            Console.WriteLine($"Child element IDs saved to '{outputJson}'.");
        }
    }

    // Recursive depth‑first search for the first element whose Tag equals "P" (paragraph)
    private static dynamic FindFirstParagraph(dynamic element)
    {
        if (element == null)
            return null;

        // Some elements expose a Tag property; compare case‑insensitively
        try
        {
            if (element.Tag != null && string.Equals(element.Tag.ToString(), "P", StringComparison.OrdinalIgnoreCase))
                return element;
        }
        catch { /* Tag property not present – ignore */ }

        // Recurse into children if the element has a ChildElements collection
        try
        {
            foreach (dynamic child in element.ChildElements)
            {
                var result = FindFirstParagraph(child);
                if (result != null)
                    return result;
            }
        }
        catch { /* No ChildElements – ignore */ }

        return null;
    }
}
