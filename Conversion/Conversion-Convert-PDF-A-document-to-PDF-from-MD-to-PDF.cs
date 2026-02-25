using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mdPath  = "input.md";
        const string pdfPath = "output.pdf";

        if (!File.Exists(mdPath))
        {
            Console.Error.WriteLine($"File not found: {mdPath}");
            return;
        }

        // Load the Markdown file with MdLoadOptions (converts it to a PDF document in memory)
        MdLoadOptions loadOptions = new MdLoadOptions();

        using (Document doc = new Document(mdPath, loadOptions))
        {
            // If the source PDF has PDF/A compliance, remove it to produce a regular PDF
            doc.RemovePdfaCompliance();

            // Save the document as a standard PDF file
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Markdown file successfully converted to PDF: {pdfPath}");
    }
}