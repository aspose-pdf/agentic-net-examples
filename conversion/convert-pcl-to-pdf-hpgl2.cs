using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL document. HP‑GL/2 vectors are loaded automatically, so no extra flag is required.
        PclLoadOptions loadOptions = new PclLoadOptions();

        using (Document doc = new Document(pclPath, loadOptions))
        {
            // Save as PDF (default format)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PCL file successfully converted to PDF: {pdfPath}");
    }
}
