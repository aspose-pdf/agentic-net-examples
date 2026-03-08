using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Output files for the different export formats
        const string fdfPath  = "export.fdf";
        const string xfdfPath = "export.xfdf";
        const string jsonPath = "export.json";
        const string xmlPath  = "export.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Form facade works directly with AcroForm fields
        using (Form form = new Form(inputPdf))
        {
            // Export fields to FDF
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }

            // Export fields to XFDF (XML‑based)
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }

            // Export fields to JSON; second argument indicates whether button values are included
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(jsonStream, false);
            }

            // Export fields to plain XML
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine("Form fields exported successfully:");
        Console.WriteLine($"FDF  -> {fdfPath}");
        Console.WriteLine($"XFDF -> {xfdfPath}");
        Console.WriteLine($"JSON -> {jsonPath}");
        Console.WriteLine($"XML  -> {xmlPath}");
    }
}