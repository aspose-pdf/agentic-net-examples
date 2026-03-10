using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (if any)
            ITaggedContent tagged = doc.TaggedContent;

            // Prepare collections for superscript and subscript characters
            List<string> superscripts = new List<string>();
            List<string> subscripts   = new List<string>();

            // The Tagged API may expose the root element as a concrete type that is not
            // available in older Aspose.Pdf versions. To keep the code version‑agnostic we
            // treat the root element (and all its children) as dynamic objects.
            dynamic root = tagged.RootElement;
            WalkStructure(root, superscripts, subscripts);

            // Output results
            Console.WriteLine("Superscript characters found:");
            foreach (string s in superscripts)
                Console.WriteLine(s);

            Console.WriteLine("\nSubscript characters found:");
            foreach (string s in subscripts)
                Console.WriteLine(s);
        }
    }

    // Recursive traversal of structure elements using dynamic typing.
    static void WalkStructure(dynamic element, List<string> supers, List<string> subs)
    {
        if (element == null)
            return;

        // Many structure elements (Paragraph, Span, Header, etc.) contain text.
        // The text is available via the ActualText property.
        // Superscript/Subscript flags are stored in the element's TextState (StructureTextState).
        try
        {
            // Guard against missing properties in older versions.
            string actualText = element.ActualText as string;
            if (!string.IsNullOrEmpty(actualText))
            {
                var textState = element.TextState; // may be null or missing
                if (textState != null)
                {
                    // The TextState object exposes Superscript and Subscript booleans.
                    bool isSup = false;
                    bool isSub = false;
                    try { isSup = textState.Superscript; } catch { }
                    try { isSub = textState.Subscript; } catch { }

                    if (isSup)
                        supers.Add(actualText);
                    if (isSub)
                        subs.Add(actualText);
                }
            }
        }
        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
        {
            // The element does not expose Expected properties – ignore and continue.
        }

        // Recurse into child elements if the collection exists.
        try
        {
            foreach (var child in element.ChildElements)
            {
                WalkStructure(child, supers, subs);
            }
        }
        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
        {
            // No ChildElements collection – nothing to recurse.
        }
    }
}
