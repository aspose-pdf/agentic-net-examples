using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath  = "FormB.pdf";
        const string xmlPath  = "FormBData.xml";
        const string jsonPath = "FormBData.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            // The Form facade implements IDisposable, ensure deterministic disposal
            using (Form form = new Form())
            {
                // Bind the PDF form to the facade
                form.BindPdf(pdfPath);

                // Import field values from the XML data file (the method expects a Stream)
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXml(xmlStream);
                }

                // Export the current field values to a JSON file.
                // ExportJson takes a Stream and an optional "indented" flag.
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportJson(jsonStream, true); // true => pretty‑printed JSON
                }
            }

            Console.WriteLine($"Form fields exported to JSON: {jsonPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
