using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, XML data and the output PDF
        const string sourcePdfPath = "template.pdf";
        const string xmlDataPath   = "data.xml";
        const string outputPdfPath = "result.pdf";

        // Verify that required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(sourcePdfPath))
        {
            // Ensure we have at least one page (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page
            Page page = doc.Pages[1];

            // Create a TextFragment that will hold multiple TextSegments
            TextFragment fragment = new TextFragment();

            // Create individual TextSegments with placeholder text
            TextSegment segment1 = new TextSegment("PLACEHOLDER_1");
            TextSegment segment2 = new TextSegment("PLACEHOLDER_2");
            TextSegment segment3 = new TextSegment("PLACEHOLDER_3");

            // Add the segments to the fragment
            fragment.Segments.Add(segment1);
            fragment.Segments.Add(segment2);
            fragment.Segments.Add(segment3);

            // Dynamically modify the TextSegment values before XML binding
            segment1.Text = "Dynamic Value A";
            segment2.Text = "Dynamic Value B";
            segment3.Text = "Dynamic Value C";

            // Optionally set font and size for the whole fragment
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 12;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Bind the external XML data to the PDF (populates any data‑bound fields)
            doc.BindXml(xmlDataPath);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}