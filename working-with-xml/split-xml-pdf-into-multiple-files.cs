using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Adjust these constants as needed
    const string XmlInputPath = "data.xml";          // Path to the large XML dataset
    const string OutputFolder = "PdfPages";          // Folder where PDFs will be written
    const int PagesPerPdf = 100;                     // Number of pages per PDF file

    static void Main()
    {
        if (!File.Exists(XmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {XmlInputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(OutputFolder);

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(XmlInputPath);

        // Assume each <Record> element represents a unit of content to be placed on a page
        XmlNodeList records = xmlDoc.SelectNodes("//Record");
        if (records == null || records.Count == 0)
        {
            Console.Error.WriteLine("No <Record> elements found in the XML.");
            return;
        }

        int pdfIndex = 1;          // Sequential PDF file number
        int pageCountInCurrent = 0;
        Document currentDoc = null;

        try
        {
            foreach (XmlNode record in records)
            {
                // Create a new Document when starting the first PDF or when the page limit is reached
                if (currentDoc == null || pageCountInCurrent >= PagesPerPdf)
                {
                    // Dispose previous document if it exists
                    if (currentDoc != null)
                    {
                        string outPath = Path.Combine(OutputFolder, $"Part_{pdfIndex:D3}.pdf");
                        currentDoc.Save(outPath);
                        Console.WriteLine($"Saved: {outPath}");
                        currentDoc.Dispose();
                        pdfIndex++;
                    }

                    // Start a new PDF document
                    currentDoc = new Document();
                    pageCountInCurrent = 0;
                }

                // Add a new page (Aspose.Pdf uses 1‑based indexing)
                Page page = currentDoc.Pages.Add();

                // Build a simple text representation of the record
                string pageText = BuildRecordText(record);

                // Create a TextFragment and add it to the page
                TextFragment fragment = new TextFragment(pageText)
                {
                    // Optional formatting
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") }
                };
                page.Paragraphs.Add(fragment);

                pageCountInCurrent++;
            }

            // Save the final document if it contains any pages
            if (currentDoc != null && currentDoc.Pages.Count > 0)
            {
                string outPath = Path.Combine(OutputFolder, $"Part_{pdfIndex:D3}.pdf");
                currentDoc.Save(outPath);
                Console.WriteLine($"Saved: {outPath}");
            }
        }
        finally
        {
            // Ensure the last document is disposed even if an exception occurs
            currentDoc?.Dispose();
        }
    }

    // Helper method to convert an XML node into a readable string
    static string BuildRecordText(XmlNode recordNode)
    {
        // Simple concatenation of child element names and values
        // Adjust formatting as required for your dataset
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("Record Details:");
        sb.AppendLine("----------------");

        foreach (XmlNode child in recordNode.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                sb.AppendLine($"{child.Name}: {child.InnerText}");
            }
        }

        return sb.ToString();
    }
}