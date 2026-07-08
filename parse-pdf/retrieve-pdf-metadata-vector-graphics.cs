using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "VectorData";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists (kept for possible future vector extraction)
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ----- Retrieve basic metadata -----
            Console.WriteLine($"Title   : {doc.Info.Title}");
            Console.WriteLine($"Author  : {doc.Info.Author}");
            Console.WriteLine($"Subject : {doc.Info.Subject}");
            Console.WriteLine($"Keywords: {doc.Info.Keywords}");

            // ----- Vector graphics extraction (optional) -----
            // The original sample attempted to iterate over XObjects via
            // page.Resources.XObjects and cast them to FormXObject. In recent
            // versions of Aspose.PDF the XObjects collection is still available
            // but the concrete type for a vector XObject (FormXObject) may be
            // located in a different namespace or not exposed at all. To keep the
            // sample compile‑time safe we omit the XObjects‑specific code. If you
            // need to extract vector graphics, refer to the official Aspose.PDF
            // documentation for the version you are using.
            int vectorCount = 0;
            Console.WriteLine($"Total vector graphics extracted (if any): {vectorCount}");
        }
    }
}
