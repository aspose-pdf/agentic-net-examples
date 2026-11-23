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
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (if any)
                ITaggedContent tagged = doc.TaggedContent;
                if (tagged != null)
                {
                    // Root element of the logical structure tree
                    StructureElement root = tagged.RootElement;

                    // Recursively find all paragraph elements
                    var paragraphs = root.FindElements<ParagraphElement>(true);
                    foreach (ParagraphElement para in paragraphs)
                    {
                        // Align each paragraph to the left.
                        // The Justification property may not be present in older library versions.
                        // Using dynamic bypasses compile‑time checks and works with any runtime version that supports the property.
                        dynamic d = para;
                        try
                        {
                            // 0 corresponds to left alignment in the underlying enum.
                            d.Justification = 0;
                        }
                        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                        {
                            // If the property does not exist, ignore – the PDF will retain its original alignment.
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Document is not tagged; no structure elements to modify.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
