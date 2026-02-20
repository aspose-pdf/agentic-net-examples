using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output SVG file path
        const string svgPath = "metadata.svg";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load PDF metadata using the Facade class PdfFileInfo
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Retrieve desired metadata properties
            string title = pdfInfo.Title ?? string.Empty;
            string author = pdfInfo.Author ?? string.Empty;
            string subject = pdfInfo.Subject ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;
            string creator = pdfInfo.Creator ?? string.Empty;
            string producer = pdfInfo.Producer ?? string.Empty;

            // PdfFileInfo returns dates as strings; convert them to nullable DateTime
            DateTime? creationDate = null;
            if (DateTime.TryParse(pdfInfo.CreationDate, out var cd))
                creationDate = cd;

            DateTime? modDate = null;
            if (DateTime.TryParse(pdfInfo.ModDate, out var md))
                modDate = md;

            int pageCount = pdfInfo.NumberOfPages;

            // Build a simple SVG document that displays the metadata
            XNamespace svgNs = "http://www.w3.org/2000/svg";
            XElement svgRoot = new XElement(svgNs + "svg",
                new XAttribute("width", "800"),
                new XAttribute("height", "600"),
                new XAttribute("xmlns", svgNs));

            // Helper to add a text line at a given vertical position
            void AddText(string content, int y)
            {
                svgRoot.Add(new XElement(svgNs + "text",
                    new XAttribute("x", "10"),
                    new XAttribute("y", y.ToString()),
                    new XAttribute("font-family", "Arial"),
                    new XAttribute("font-size", "14"),
                    content));
            }

            int line = 30;
            AddText($"Title: {title}", line); line += 20;
            AddText($"Author: {author}", line); line += 20;
            AddText($"Subject: {subject}", line); line += 20;
            AddText($"Keywords: {keywords}", line); line += 20;
            AddText($"Creator: {creator}", line); line += 20;
            AddText($"Producer: {producer}", line); line += 20;
            AddText($"Creation Date: {creationDate?.ToString("u") ?? "N/A"}", line); line += 20;
            AddText($"Modification Date: {modDate?.ToString("u") ?? "N/A"}", line); line += 20;
            AddText($"Page Count: {pageCount}", line);

            // Save the SVG content to file
            XDocument svgDoc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), svgRoot);
            svgDoc.Save(svgPath);

            Console.WriteLine($"Metadata extracted and saved to SVG file '{svgPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
