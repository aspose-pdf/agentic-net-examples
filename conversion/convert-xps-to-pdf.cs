using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (load‑option classes are here)

class Program
{
    static void Main()
    {
        const string xpsPath = "input.xps";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        // Load the XPS document using XpsLoadOptions (no extra using needed)
        XpsLoadOptions loadOptions = new XpsLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(xpsPath, loadOptions))
        {
            // Save the loaded document as PDF (default format)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"XPS converted to PDF successfully: {pdfPath}");
    }
}
