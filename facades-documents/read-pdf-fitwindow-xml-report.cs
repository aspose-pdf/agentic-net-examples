using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf; // high‑level API provides viewer‑preference properties

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlReportPath = "report.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF with the high‑level Document API to read viewer preferences.
        bool fitWindow;
        Document pdfDoc = new Document(pdfPath);
        // The FitWindow flag is part of the Document viewer‑preference settings.
        fitWindow = pdfDoc.FitWindow;

        // Build a simple XML report containing the result.
        XDocument report = new XDocument(
            new XElement("PdfReport",
                new XElement("FileName", Path.GetFileName(pdfPath)),
                new XElement("FitWindow", fitWindow)
            )
        );

        // Save the XML report to disk.
        report.Save(xmlReportPath);
        Console.WriteLine($"Report saved to '{xmlReportPath}'. FitWindow = {fitWindow}");
    }
}
