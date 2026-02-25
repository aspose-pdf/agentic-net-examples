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
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Load options for PCL conversion
        PclLoadOptions loadOptions = new PclLoadOptions();
        // Uncomment to use the new conversion engine if needed
        // loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

        // Load the PCL file; Aspose.Pdf treats it as a PDF document internally
        using (Document doc = new Document(pclPath, loadOptions))
        {
            // If the loaded document has PDF/A compliance, remove it to get a regular PDF
            doc.RemovePdfaCompliance();

            // Save the result as a standard PDF file
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PCL successfully converted to PDF: {pdfPath}");
    }
}