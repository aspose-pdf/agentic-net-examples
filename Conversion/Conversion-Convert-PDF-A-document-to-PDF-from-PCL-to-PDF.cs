using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL file with default options.
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Use a using block to ensure the Document is disposed properly.
        using (Document doc = new Document(pclPath, loadOptions))
        {
            // If the conversion produced a PDF/A document, remove the compliance
            // to obtain a regular PDF. Calling this method on a non‑PDF/A file is safe.
            doc.RemovePdfaCompliance();

            // Save the result as a standard PDF file.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PCL file has been converted to PDF: {outputPdf}");
    }
}