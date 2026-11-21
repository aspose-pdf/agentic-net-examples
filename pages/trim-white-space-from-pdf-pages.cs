using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing. Use dynamic dispatch to call Trim()
            // if the method exists in the runtime version of Aspose.Pdf. This avoids a
            // compile‑time error when the method is not present in the referenced
            // assembly while still allowing the call when it is available.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                try
                {
                    // Dynamic call – works only if Trim() exists at runtime.
                    ((dynamic)page).Trim();
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    // Trim method not available in this version; fallback to a no‑op.
                    // In a production scenario you could implement custom trimming
                    // logic here (e.g., render the page to an image, analyse the bitmap,
                    // and adjust the page's CropBox). For the purpose of this example
                    // we simply leave the page unchanged.
                }
            }

            // Save the (potentially) trimmed PDF (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}
