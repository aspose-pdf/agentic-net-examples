using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (change as needed)
        const string inputPath = "input.pdf";
        // Output PDF path where the (potentially) accessibility‑enhanced PDF will be saved
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // -----------------------------------------------------------------
            // Attempt to work with accessibility (tagged) content via reflection.
            // This avoids compile‑time errors on library versions that do not
            // expose the TaggedContent API.
            // -----------------------------------------------------------------
            PropertyInfo taggedProp = typeof(Document).GetProperty("TaggedContent", BindingFlags.Instance | BindingFlags.Public);
            if (taggedProp != null)
            {
                // The property exists – obtain the ITaggedContent instance
                object taggedContent = taggedProp.GetValue(pdfDocument);
                if (taggedContent != null)
                {
                    // Call PreSave() and Save() using reflection
                    MethodInfo preSave = taggedContent.GetType().GetMethod("PreSave", BindingFlags.Instance | BindingFlags.Public);
                    MethodInfo save = taggedContent.GetType().GetMethod("Save", BindingFlags.Instance | BindingFlags.Public);

                    preSave?.Invoke(taggedContent, null);
                    save?.Invoke(taggedContent, null);
                }
            }

            // Save the (possibly modified) PDF to the output file
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}