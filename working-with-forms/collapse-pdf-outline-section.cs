using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Annotations;        // For annotation types if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "collapsed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one outline (bookmark) collection
            if (doc.Outlines != null && doc.Outlines.Count > 0)
            {
                // Set the first outline item's Open property to false (collapsed)
                // OutlineItemCollection.Open controls the initial expand/collapse state.
                doc.Outlines[1].Open = false;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with collapsed outline: '{outputPath}'");
    }
}